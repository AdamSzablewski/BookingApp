using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp;
[ApiController]
[Authorize]
[Route("facility")]
public class FacilityController(FacilityService facilityService, SecurityService securityService) : ControllerBase
{
    private readonly FacilityService _facilityService = facilityService;
    private readonly SecurityService _securityService = securityService;

    [HttpGet("{facilityId:long}")]
    public async Task<IActionResult> GetById([FromRoute]long facilityId)
    {
        var facility = await _facilityService.GetById(facilityId);
        if(facility == null)
        {
            return NotFound();
        }
        return Ok(facility);
    }

    [HttpPost("{facilityId}")]
    public async Task<IActionResult> Create([FromRoute]string facilityId, [FromBody]FacilityCreateDto dto)
    {
        if(!ModelState.IsValid){ return BadRequest(ModelState);};
        var facility = await _facilityService.CreateAsync(facilityId, dto);
        if(facility == null)
        {
            return BadRequest("Failed to create the facility");
        }
        return CreatedAtAction(nameof(GetById), facility.Id, facility);
    }
}
