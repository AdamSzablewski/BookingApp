using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp;
[ApiController]
[Authorize]
[Route("service")]
public class ServiceController : ControllerBase
{
    private readonly ServiceService _serviceService;
    private readonly SecurityService _securityService;

    public ServiceController(ServiceService serviceService, SecurityService securityService)
    {
        _serviceService = serviceService ?? throw new ArgumentNullException(nameof(serviceService));
        _securityService = securityService ?? throw new ArgumentNullException(nameof(serviceService));
    }

    [HttpGet("{serviceId:long}")]
    public async Task<IActionResult> GetById([FromRoute] long serviceId){
        var service = await _serviceService.GetByIdAsync(serviceId);
        if(service == null)
        {
            return NotFound($"Service with id: {serviceId} was not found");
        }
        return Ok(service);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromQuery] long facilityId,[FromBody] ServiceCreateDto serviceCreateDto)
    {
        var createdService = await _serviceService.CreateAsync(facilityId, serviceCreateDto);
        return CreatedAtAction(nameof(GetById), new {serviceId = createdService.Id}, createdService.MapToDto());
    }

    [HttpPut("{serviceId:long}")]
    public async Task<IActionResult> Update([FromRoute] long serviceId, [FromBody] ServiceCreateDto serviceDto)
    {
        bool authenticated = await _securityService.IsOwner(HttpContext, serviceId);
        if(!authenticated)
        {
            return Unauthorized();
        }
        var updatedService = await _serviceService.UpdateAsync(serviceId, serviceDto);
        if(updatedService == null)
        {
            return NotFound("Service not found");
        }
        return Ok(updatedService);
    }

    [HttpPatch("name/{serviceId:long}")]
    public async Task<IActionResult> ChangeName([FromRoute] long serviceId, [FromQuery] string newName)
    {
        bool authenticated = await _securityService.IsOwner(HttpContext, serviceId);
        if(!authenticated)
        {
            return Unauthorized();
        }
        var service = await _serviceService.ChangeNameAsync(serviceId, newName);
        if(service == null)
        {
            return NotFound("Service was not found");
        }
        return Ok(service);
    }

    [HttpPatch("{serviceId:long}")]
    public async Task<IActionResult> ChangePrice([FromRoute] long serviceId, [FromQuery] decimal newPrice)
    {
        bool authenticated = await _securityService.IsOwner(HttpContext, serviceId);
        if(!authenticated)
        {
            return Unauthorized();
        }
        return Ok(await _serviceService.ChangePriceAsync(serviceId, newPrice));
    }

    [HttpPut("employee")]
    public async Task<IActionResult> AddEmployeeToService([FromQuery] long employeeId, [FromQuery] long serviceId)
    {
        bool authenticated = await _securityService.IsOwnerOfService(HttpContext, serviceId);
        Console.WriteLine(authenticated);
        if(!authenticated)
        {
            return Unauthorized();
        }
        var service = await _serviceService.AddEmployeeToService(employeeId, serviceId);
        if(service == null)
        {
            return NotFound("Service was not found");
        }
        return Ok(service);
    }

}
