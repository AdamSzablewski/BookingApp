using Microsoft.AspNetCore.Mvc;

namespace BookingApp;
[ApiController]
[Route("facility")]
public class FacilityController : ControllerBase
{
    private readonly FacilityService _facilityService;

    public FacilityController(FacilityService facilityService){
        _facilityService = facilityService;
    }
    [HttpGet("{Id}")]
    public IActionResult getById([FromRoute]long Id){
        return Ok(_facilityService.GetById(Id));
    }
    [HttpPost("{Id}")]
    public IActionResult create([FromRoute]long Id, [FromBody]FacilityCreateDto dto){
        _facilityService.Create(Id, dto);
        return Ok();
    }
}
