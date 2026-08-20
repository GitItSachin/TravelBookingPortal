using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Services;
using TravelBookingPortal.Api.Dtos.Branch;

namespace TravelBookingPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchesController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Branch>>> GetAllBranches()
        {
            var branches = await _branchService.GetAllBranchesAsync();
            return Ok(branches);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Branch>> GetBranchById(int id)
        {
            var branch = await _branchService.GetBranchByIdAsync(id);
            if (branch == null)
                return NotFound();

            return Ok(branch);
        }

        [HttpPost]
        public async Task<ActionResult<Branch>> CreateBranch([FromBody] CreateBranchDto createBranchDto)
        {
            var createdBranch = await _branchService.CreateBranchAsync(createBranchDto);
            return CreatedAtAction(nameof(GetBranchById), new { id = createdBranch.Id }, createdBranch);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Branch>> UpdateBranch(int id, [FromBody] UpdateBranchDto updateBranchDto)
        {
            var updatedBranch = await _branchService.UpdateBranchAsync(id, updateBranchDto);
            if (updatedBranch == null)
                return NotFound();

            return Ok(updatedBranch);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBranch(int id)
        {
            var result = await _branchService.DeleteBranchAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult<Branch>> PatchBranch(int id, [FromBody] BranchPatchDto patch)
        {
            var updatedBranch = await _branchService.PatchBranchAsync(id, patch);
            if (updatedBranch == null)
                return NotFound();

            return Ok(updatedBranch);
        }
    }
}
