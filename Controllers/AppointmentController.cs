﻿using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp;
[ApiController]
[Authorize]
[Route("appointment")]
public class AppointmentController(AppointmentService appointmentService, SecurityService securityService) : ControllerBase
{
    private readonly AppointmentService _appointmentService = appointmentService;
    private readonly SecurityService _securityService = securityService;

    [HttpGet]
    public async Task<IActionResult> GetTimeslotsForDay([FromQuery] long serviceId, [FromQuery] string date)
    {
        DateOnly formattedDate = DateOnly.ParseExact(date, "dd/MM/yyyy");
        return Ok(await _appointmentService.GetAvailableTimeSlotsForService(serviceId, formattedDate));
    }
    [HttpGet("latest")]
    public async Task<IActionResult> GetLatestAppointment([FromQuery] string userId)
    {
        
        return Ok(await _appointmentService.GetLatestAppointment(userId));
    }

    [HttpPost]
    public async Task<IActionResult> BookAppointment([FromBody] AppointmentCreateDto appointmentCreateDto)
    {   
        // Console.WriteLine("------------------before");
        //  if(!ModelState.IsValid){ return BadRequest(ModelState);};
        //         Console.WriteLine("after");

        var userId = _securityService.GetUserIdFromRequest(HttpContext);
        var appointment = await _appointmentService.BookAppointment(appointmentCreateDto, userId);
        if(appointment == null)
        {
            return BadRequest("Failed to book the appointment");
        }
        return Ok(appointment);
    }

    [HttpGet("cancel")]
    public async Task<IActionResult> CancelAppointment([FromQuery] long appointmentId, [FromQuery] string userId)
    {
        _securityService.IsUser(HttpContext, userId);
        bool canceledAppointment = await _appointmentService.CancelAppointment(appointmentId);
        if(!canceledAppointment)
        {
            return BadRequest("Failed to cancel the appointment");
        }
        return Ok();
    }
}
