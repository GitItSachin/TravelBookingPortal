using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Services;
using TravelBookingPortal.Api.Dtos.Staff;

namespace TravelBookingPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffsController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetAllStaff()
        {
            var staff = await _staffService.GetAllStaffAsync();
            return Ok(staff);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaffById(int id)
        {
            var s = await _staffService.GetStaffByIdAsync(id);
            if (s == null) return NotFound();
            return Ok(s);
        }

        [HttpPost]
        public async Task<ActionResult<Staff>> CreateStaff([FromBody] CreateStaffDto createDto)
        {
            var created = await _staffService.CreateStaffAsync(createDto);
            return CreatedAtAction(nameof(GetStaffById), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Staff>> UpdateStaff(int id, [FromBody] UpdateStaffDto updateDto)
        {
            var updated = await _staffService.UpdateStaffAsync(id, updateDto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStaff(int id)
        {
            var result = await _staffService.DeleteStaffAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult<Staff>> PatchStaff(int id, [FromBody] StaffPatchDto patch)
        {
            var updated = await _staffService.PatchStaffAsync(id, patch);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
    }
}
