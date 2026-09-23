# .NET Full Stack Developer — Interview Prep Cheat Sheet
Southlake, TX | 6–10 yrs | Tomorrow 3 PM

Prioritized per your input: Core .NET/C#/EF/SOLID, Angular/NgRx/testing, Behavioral/system design. (Splunk/GitHub Actions/Bamboo skipped for now — happy to add a quick pass later if time allows.)

---

## 1. Core C# / .NET

**Async/await** — `async`/`await` doesn't create new threads; it frees the calling thread while I/O-bound work happens, then resumes on a captured context (or a thread-pool thread in ASP.NET Core, since there's no `SynchronizationContext` there). Common trap: mixing `.Result`/`.Wait()` with `async` causes deadlocks in contexts with a sync context (classic ASP.NET, UI apps) — less of an issue in ASP.NET Core but still bad practice. Know the difference between `Task` and `Task<T>`, and `ValueTask` for high-throughput hot paths avoiding allocation.

**LINQ deferred execution** — Most LINQ operators (`Where`, `Select`) build an expression tree/iterator but don't execute until enumerated (`ToList()`, `foreach`, `Count()` without predicate variants aside). Interview trap: modifying a collection after building a deferred query but before enumerating it changes the results. `IQueryable` vs `IEnumerable`: `IQueryable` translates to SQL (EF), `IEnumerable` runs in memory — moving a filter from `Where` on `IQueryable` to after `.ToList()` moves the work from DB to app server.

**Delegates & events** — A delegate is a type-safe function pointer; `Action`, `Func`, `Predicate` are built-in generic delegates. Events are a publish/subscribe wrapper around multicast delegates, restricting external code to only `+=`/`-=`, not invoking or reassigning.

**Generics** — Type safety without boxing/casting, `where T : class/struct/new()/IInterface` constraints. Know covariance (`out`) and contravariance (`in`) on generic interfaces (`IEnumerable<out T>`).

**Nullable reference types** (`#nullable enable`) — Compile-time warnings, not runtime null safety. Know `?`, `!` (null-forgiving operator), and how this interacts with EF entities (often needs `required` or constructor init, as in your `Staff.Name`/`Email` using `required string`).

**Records vs classes** — Records give value-based equality and immutability-by-default (`init` setters), good for DTOs. Classes are reference-equality by default. `record class` vs `record struct` (C# 10+).

**DI lifetimes** — `Singleton` (one instance app-wide), `Scoped` (one per HTTP request), `Transient` (new every resolution). Classic bug: injecting a `Scoped` service into a `Singleton` — captive dependency, throws or silently misbehaves. This matches your own project structure — `AddScoped<IStaffRepository, StaffRepository>()` etc.

**Middleware pipeline** — Order matters: `UseCors` before `UseAuthentication`/`UseAuthorization` (as you just fixed), exception handling middleware wraps everything (`app.UseMiddleware<ExceptionHandlingMiddleware>()` early, like in your project). Each middleware can short-circuit or call `next()`.

**Likely questions:** "Explain async/await under the hood." "Difference between IEnumerable and IQueryable, and why does it matter for EF?" "What's a captive dependency?" "When would you use a record over a class?"

---

## 2. Entity Framework

**Tracking vs no-tracking** — By default EF tracks entities for change detection; `.AsNoTracking()` skips the tracker (faster, use for read-only queries — GET endpoints). You should mention this explicitly since your `GetAllBookingsAsync`-style methods are prime candidates for `.AsNoTracking()` if not already applied.

**N+1 problem** — Looping over a collection and lazy-loading a related entity per iteration issues one query per row. Fix with eager loading (`.Include()`), or projecting to a DTO with a single query. Given your project loads `Staff.Include(s => s.Branch)`, that's the right pattern — be ready to explain *why* you did that.

**Loading strategies** — Eager (`.Include`), explicit (`.Entry().Reference().Load()`), lazy (proxies, needs virtual nav properties + `UseLazyLoadingProxies`) — lazy loading is often discouraged in APIs because it can trigger N+1 silently during serialization.

**Migrations** — `Add-Migration`/`dotnet ef migrations add`, `Update-Database`/`dotnet ef database update`. Know the difference between `Migrate()` at startup (auto-applies, risky in prod with multiple instances racing) vs a separate deploy step.

**Optimistic concurrency** — `[Timestamp]`/`RowVersion` column, `DbUpdateConcurrencyException` on conflict.

**Performance tuning talking points** — `AsNoTracking`, compiled queries (`EF.CompileQuery`) for hot paths, `SaveChanges` batching, avoiding client-side evaluation (a `Where` clause EF can't translate silently pulls everything into memory — watch for warnings in logs), proper indexing on FK/filter columns, pagination via `Skip`/`Take` with `OrderBy` (unordered paging is non-deterministic).

**Likely questions:** "How would you fix an N+1 problem you find in production?" "AsNoTracking — what's the tradeoff?" "How do migrations work in a CI/CD pipeline with multiple instances?"

---

## 3. SOLID Principles (have a C# example ready for each)

**S — Single Responsibility.** A class should have one reason to change. Your own `AuthService` handling only auth logic, separate from `StaffService`, is a live example you can cite from your own project.

**O — Open/Closed.** Open for extension, closed for modification — e.g., strategy pattern instead of a growing `switch` statement.

**L — Liskov Substitution.** Subtypes must be substitutable for their base type without breaking behavior — classic bad example: `Square : Rectangle` overriding `SetWidth` breaks expectations.

**I — Interface Segregation.** Prefer several small interfaces over one fat one — e.g., `IStaffRepository` vs one giant `IRepository` with unrelated methods.

**D — Dependency Inversion.** High-level modules depend on abstractions, not concretions — your controllers depend on `IBookingService`, not `BookingService` directly, injected via DI. Good concrete example straight from your own code.

**Likely questions:** "Give me a real example from your own code where you applied SRP or DIP." (You have one — use your own `TravelBookingPortal` layering: Controller → Service → Repository, all behind interfaces.)

---

## 4. Design Patterns

**Repository + Unit of Work** — Abstracts data access; UoW coordinates multiple repository changes in one transaction/`SaveChanges` call. Be ready to discuss the criticism (EF's `DbContext` already *is* a Unit of Work/Repository, so an extra layer can be redundant) — a thoughtful answer acknowledging tradeoffs plays well.

**Factory** — Centralizes object creation logic, useful when construction is complex or depends on runtime conditions.

**Singleton** — One instance app-wide; in .NET, usually via DI container (`AddSingleton`) rather than the classic static-instance pattern, since DI-managed singletons are more testable.

**Strategy** — Swap algorithms at runtime behind a common interface — good answer for "how would you avoid a big if/else chain."

**Decorator** — Wrap an object to add behavior without modifying it — e.g., a logging or caching decorator around a service, or ASP.NET Core middleware itself is essentially decorator-pattern-shaped.

---

## 5. Unit & Integration Testing (.NET)

**Frameworks** — xUnit is most common in modern .NET (also NUnit/MSTest). Know `[Fact]` vs `[Theory]`/`[InlineData]` in xUnit.

**Mocking** — Moq is standard: `Mock<IStaffRepository>`, `.Setup(...)`, `.Returns(...)`, `.Verify(...)`. AAA pattern: Arrange, Act, Assert.

**Integration testing** — `WebApplicationFactory<TEntryPoint>` spins up an in-memory test server hitting real middleware/routing, often paired with an in-memory or test SQL database (`UseInMemoryDatabase` for speed, or a real SQL Server test instance for fidelity — mention the tradeoff: in-memory provider doesn't enforce all real SQL Server constraints).

**Likely questions:** "How do you unit test a controller that depends on a service?" (Mock the interface, inject the mock, assert the returned `ActionResult` type/status code.) "How would you test your `ExceptionHandlingMiddleware`?" (Integration test hitting an endpoint that throws, asserting the JSON shape and status code.)

---

## 6. API Design & Performance Tuning

REST conventions (verbs, status codes — you've got a clean example with your `BookingsController`: 200/201/204/404/401). Versioning strategies (URL segment, header, query string). Caching: response caching middleware, `ETag`/`If-None-Match`, output caching (new in .NET 7+). Async all the way down for I/O-bound controller actions (you're already doing this). Pagination for list endpoints (your `GetAllBookings`-style endpoints are candidates to mention adding pagination to, tying into your own Day 12 roadmap item). Rate limiting (`Microsoft.AspNetCore.RateLimiting` in .NET 7+).

---

## 7. Angular

**Routing** — `Routes` array, `RouterModule.forRoot`/`forChild`, route guards (`canActivate`, `canDeactivate`, `canMatch`) — you just built a functional `CanActivateFn` guard, use that as a live example. Lazy loading via `loadChildren` for feature modules — improves initial bundle size, worth mentioning even though your current app doesn't need it yet at this scale.

**Directives** — Structural (`*ngIf`, `*ngFor`, `*ngSwitch` — change the DOM structure, prefixed with `*`) vs attribute (`ngClass`, `ngStyle` — change appearance/behavior of an existing element). You can also describe writing a custom attribute directive (`@Directive`, `ElementRef`, `Renderer2`).

**Pipes** — Transform data in templates (`{{ value | pipe }}`). Built-in: `date`, `currency`, `async` (auto-subscribes/unsubscribes observables — good one to highlight, avoids manual subscription leaks). Pure vs impure pipes: pure pipes only re-run when input reference changes (performance-friendly, default), impure re-run every change detection cycle (expensive, needed for mutable arrays/objects).

**Components** — Lifecycle hooks in order: `ngOnChanges` → `ngOnInit` → `ngDoCheck` → `ngAfterContentInit` → `ngAfterContentChecked` → `ngAfterViewInit` → `ngAfterViewChecked` → `ngOnDestroy`. `@Input`/`@Output` for parent-child communication, `@ViewChild`/`@ContentChild` for querying DOM/child components. Change detection: default (`CheckAlways`) vs `OnPush` (only re-checks on `@Input` reference change, `Observable` emission via async pipe, or explicit `markForCheck` — big performance lever, good to mention proactively).

**NgRx** — Redux pattern for Angular: single immutable `Store`, `Actions` (plain objects describing what happened), `Reducers` (pure functions, `state -> newState`), `Effects` (side effects like HTTP calls, listen for actions via `Actions` stream, dispatch new actions), `Selectors` (memoized state slices via `createSelector`). When to reach for it vs. plain services + RxJS `BehaviorSubject`: NgRx pays off when state is shared across many unrelated components, has complex derived/computed slices, or needs strict traceability (time-travel debugging) — for a small app, a shared service with a `BehaviorSubject` is often simpler and the more honest answer if you haven't used NgRx heavily day-to-day.

**Jasmine/Karma** — Jasmine is the testing framework (`describe`, `it`, `expect`, `spyOn`), Karma is the test runner that launches a real browser and reports results back. `TestBed.configureTestingModule` sets up a testing Angular module; `ComponentFixture` wraps a component instance + its DOM for assertions; `fixture.detectChanges()` triggers change detection manually in tests.

**Likely questions:** "Walk me through your login flow guard." (You can literally describe the `authGuard`/`canActivate` you just built.) "OnPush vs default change detection — when would you use it?" "Why async pipe over manual subscribe?"

---

## 8. Behavioral / System Design

Use STAR (Situation, Task, Action, Result) for behavioral answers — keep each under ~90 seconds, lead with the result if asked to be brief.

Have one story ready for each of: a performance problem you diagnosed and fixed (an N+1 query, a slow endpoint), a disagreement with a teammate/PM you navigated, a production incident and your troubleshooting process, and a time you learned a new tool/technology under time pressure (genuinely relevant here — you can honestly reference how quickly you're picking up Angular right now).

**System design / scalability talking points** — Horizontal scaling stateless APIs behind a load balancer, caching layers (Redis for distributed cache vs in-memory), async processing/queues for long-running work, database indexing and read replicas, circuit breakers/retries for downstream calls (Polly library in .NET).

**Questions to ask them** — team structure and on-call expectations, what a typical sprint/release cycle looks like, how they use Splunk/monitoring day to day, what "success" looks like in the first 90 days.

---

## Quick self-check before the call
Re-read your own `AuthService.cs`, `ExceptionHandlingMiddleware.cs`, and the `authGuard` you just wrote — they're real, defensible examples for DI, middleware ordering, SOLID, and Angular guards respectively. Interviewers respond well to "here's something I actually built" over textbook definitions.
