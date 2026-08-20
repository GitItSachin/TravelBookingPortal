import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email = '';
  password = '';
  errorMessage = '';

  constructor(private http: HttpClient, private router: Router) {}

  onSubmit() {
    this.errorMessage = '';
    this.http.post('http://localhost:5025/api/auth/login', { 
    //this.http.post('https://localhost:7222/api/auth/login', { 
      email: this.email, 
      password: this.password })
      .subscribe({
        next: (response) => {
          console.log('Login successful:', response);
          localStorage.setItem( 'token', (response as any).token);
          this.router.navigate(['/dashboard']);
        },
        error: (error) => {
          this.errorMessage = error.error?.message || 'Invalid email or password.';
        }
      });
  }
}
