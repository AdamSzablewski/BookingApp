﻿using Microsoft.AspNetCore.Mvc;

namespace BookingApp;
[ApiController]
[Route("appointment")]
public class AppointmentController : ControllerBase
{
    private readonly AppointmentService _appointmentService;
    public AppointmentController(AppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }
    [HttpGet]
    public async Task<IActionResult> GetTimesoltsForDay([FromQuery] long serviceId, [FromQuery] string date)
    {
        return Ok(await _appointmentService.GetAvailableTimeSlotsForService(serviceId, DateOnly.Parse(date)));
    }
    [HttpPost]
    public async Task<IActionResult> BookAppointment(AppointmentCreateDto appointment)
    {
        return Ok(await _appointmentService.BookAppointment(appointment));
    }
    [HttpGet("cancel")]
    public async Task<IActionResult> CancelAppointment([FromQuery] long appointmentId)
    {
        return Ok(await _appointmentService.CancelAppointment(appointmentId));
    }
}