using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Services;
using TravelBookingPortal.Api.Dtos.TourPackage;

namespace TravelBookingPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TourPackagesController : ControllerBase
    {
        private readonly ITourPackageService _tourPackageService;

        public TourPackagesController(ITourPackageService tourPackageService)
        {
            _tourPackageService = tourPackageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourPackage>>> GetAllTourPackages()
        {
            var list = await _tourPackageService.GetAllTourPackagesAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TourPackage>> GetTourPackageById(int id)
        {
            var tp = await _tourPackageService.GetTourPackageByIdAsync(id);
            if (tp == null) return NotFound();
            return Ok(tp);
        }

        [HttpPost]
        public async Task<ActionResult<TourPackage>> CreateTourPackage([FromBody] CreateTourPackageDto createDto)
        {
            var created = await _tourPackageService.CreateTourPackageAsync(createDto);
            return CreatedAtAction(nameof(GetTourPackageById), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<TourPackage>> UpdateTourPackage(int id, [FromBody] UpdateTourPackageDto updateDto)
        {
            var updated = await _tourPackageService.UpdateTourPackageAsync(id, updateDto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTourPackage(int id)
        {
            var result = await _tourPackageService.DeleteTourPackageAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult<TourPackage>> PatchTourPackage(int id, [FromBody] TourPackagePatchDto patch)
        {
            var updated = await _tourPackageService.PatchTourPackageAsync(id, patch);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
    }
}
