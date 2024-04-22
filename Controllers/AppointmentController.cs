using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp;
[ApiController]
[Authorize]
[Route("appointment")]
public class AppointmentController : ControllerBase
{
    private readonly AppointmentService _appointmentService;
    private readonly SecurityService _securityService;
    public AppointmentController(AppointmentService appointmentService, SecurityService securityService)
    {
        _appointmentService = appointmentService;
        _securityService = securityService;
    }
    [HttpGet]
    public async Task<IActionResult> GetTimesoltsForDay([FromQuery] long serviceId, [FromQuery] string date)
    {
        if(!ModelState.IsValid){ return BadRequest(ModelState);};
        return Ok(await _appointmentService.GetAvailableTimeSlotsForService(serviceId, DateOnly.Parse(date)));
    }
    [HttpPost]
    public async Task<IActionResult> BookAppointment([FromBody] AppointmentCreateDto appointment, [FromQuery] string userId)
    {
        if(!ModelState.IsValid){ return BadRequest(ModelState);};
        _securityService.IsUser(HttpContext, appointment.UserId);
        return Ok(await _appointmentService.BookAppointment(appointment));
    }
    [HttpGet("cancel")]
    public async Task<IActionResult> CancelAppointment([FromQuery] long appointmentId, [FromQuery] string userId)
    {
        _securityService.IsUser(HttpContext, userId);
        return Ok(await _appointmentService.CancelAppointment(appointmentId));
    }
}
