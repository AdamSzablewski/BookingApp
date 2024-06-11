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

    [HttpGet]
    public async Task<IActionResult> GetFacilitiesByCriteria([FromQuery] string country, [FromQuery] string city, [FromQuery] string category)
    {
        var facilities = await _facilityService.GetFacilitiesByCriteria(country, city, category);
        Console.WriteLine($"facilites /////// ----: {facilities.Count}");
        if(facilities == null)
        {
            return NotFound();
        }
        return Ok(facilities);
    }
    [HttpGet("{facilityId}")]
    public async Task<IActionResult> GetById([FromRoute]long facilityId)
    {
        var facility = await _facilityService.GetById(facilityId);
        if(facility == null)
        {
            return NotFound();
        }
        return Ok(facility.MapToDto());
    }
    [HttpGet("picture/{facilityId}")]
    public async Task<IActionResult> GetImgForFacility([FromRoute]long facilityId)
    {
        var facility = await _facilityService.GetById(facilityId);
        if(facility == null)
        {
            return NotFound();
        }
        return Ok(facility.ImgUrl);
    }
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetFacilityForUser([FromRoute] string userId)
    {
        var facilities = await _facilityService.GetForUser(userId);
        if(facilities == null)
        {
            return NotFound();
        }
        return Ok(facilities);
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> Create([FromRoute]string userId, [FromBody]FacilityCreateDto dto)
    {
        if(!ModelState.IsValid){ return BadRequest(ModelState);};
        var facility = await _facilityService.CreateAsync(userId, dto);
        if(facility == null)
        {
            return BadRequest("Failed to create the facility");
        }
        return CreatedAtAction(nameof(GetById), new { facilityId = facility.Id}, facility.MapToDto());
    }
}
