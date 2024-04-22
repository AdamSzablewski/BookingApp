using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp;

[ApiController]
[Authorize]
[Route("employment")]
public class EmployemntController : ControllerBase
{
    private readonly EmploymentService _employmentService;
    public EmployemntController(EmploymentService employmentService){
        _employmentService = employmentService;
    }
    [HttpPost]
    public async Task<IActionResult> SendEmploymentRequest([FromBody] EmploymentRequestDto employmentRequestDto)
    {
        if(!ModelState.IsValid){return BadRequest(ModelState);};
        await _employmentService.SendEmploymentRequest(employmentRequestDto);
        return Ok();
    }
    [HttpPost("answere")]
    public async Task<IActionResult> AnswereEmploymentRequest([FromQuery] long requestId, [FromQuery] bool decision, [FromQuery] string userId)
    {   
        await _employmentService.AnswereEmploymentRequest(requestId, userId, decision);
        return Ok();
    }

}
