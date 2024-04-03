﻿using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetById([FromRoute]long Id){
        return Ok(await _facilityService.GetById(Id));
    }
    [HttpPost("{Id}")]
    public async Task<IActionResult> Create([FromRoute]long Id, [FromBody]FacilityCreateDto dto){
        await _facilityService.CreateAsync(Id, dto);
        return Ok();
    }
}
