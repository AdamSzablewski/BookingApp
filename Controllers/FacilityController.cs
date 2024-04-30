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

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute]long Id){
        return Ok(await _facilityService.GetById(Id));
    }
    // [HttpPut("{Id}")]
    // public async Task<IActionResult> Update([FromRoute]long Id, [FromBody] FaciltyUpdateDto facilityUpdateDto,[FromQuery] string userId)
    // {
    //     if(!ModelState.IsValid){ return BadRequest(ModelState);};
    //     _securityService.IsOwner(HttpContext, Id, userId);
    //     return Ok(await _facilityService.Update(Id));
    // }
    [HttpPost("{Id}")]
    public async Task<IActionResult> Create([FromRoute]string Id, [FromBody]FacilityCreateDto dto){
        await _facilityService.CreateAsync(Id, dto);
        return Ok();
    }
}
