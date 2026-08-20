using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPortal.Api.Models;
using TravelBookingPortal.Api.Services;
using TravelBookingPortal.Api.Dtos.Destination;

namespace TravelBookingPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationService _destinationService;

        public DestinationsController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Destination>>> GetAllDestinations()
        {
            var destinations = await _destinationService.GetAllDestinationsAsync();
            return Ok(destinations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Destination>> GetDestinationById(int id)
        {
            var destination = await _destinationService.GetDestinationByIdAsync(id);
            if (destination == null)
                return NotFound();

            return Ok(destination);
        }

        [HttpPost]
        public async Task<ActionResult<Destination>> CreateDestination([FromBody] CreateDestinationDto createDto)
        {
            var created = await _destinationService.CreateDestinationAsync(createDto);
            return CreatedAtAction(nameof(GetDestinationById), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Destination>> UpdateDestination(int id, [FromBody] UpdateDestinationDto updateDto)
        {
            var updated = await _destinationService.UpdateDestinationAsync(id, updateDto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDestination(int id)
        {
            var result = await _destinationService.DeleteDestinationAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult<Destination>> PatchDestination(int id, [FromBody] DestinationPatchDto patch)
        {
            var updated = await _destinationService.PatchDestinationAsync(id, patch);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }
    }
}
