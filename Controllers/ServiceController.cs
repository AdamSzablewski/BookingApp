using Microsoft.AspNetCore.Mvc;

namespace BookingApp;
[ApiController]
[Route("service")]
public class ServiceController : ControllerBase
{
    private readonly ServiceService _serviceService;

    public ServiceController(ServiceService serviceService){
        _serviceService = serviceService;
    }
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] long Id){
        return Ok(await _serviceService.GetByIdAsync(Id));
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromQuery] long FacilityId,[FromBody] ServiceCreateDto serviceCreateDto){
        return Ok( await _serviceService.CreateAsync(FacilityId, serviceCreateDto));
    }
    [HttpPut("{Id}")]
    public async Task<IActionResult> Update([FromRoute] long Id, [FromBody] ServiceCreateDto serviceDto){
        return Ok( await _serviceService.UpdateAsync(Id, serviceDto));
    }
    [HttpPatch("name/{Id}")]
    public async Task<IActionResult> ChangeName([FromRoute] long Id, [FromQuery] string newName){
        return Ok(await _serviceService.ChangeNameAsync(Id, newName));
    }
    [HttpPatch("price/{Id}")]
    public async Task<IActionResult> ChangePrice([FromRoute] long Id, [FromQuery] decimal newPrice){
        return Ok(await _serviceService.ChangePriceAsync(Id, newPrice));
    }

}
