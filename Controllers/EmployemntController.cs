using Microsoft.AspNetCore.Mvc;

namespace BookingApp;

[ApiController]
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
        return Ok(await _employmentService.SendEmploymentRequest(employmentRequestDto));
    }
    [HttpPost("answere")]
    public async Task<IActionResult> AnswereEmploymentRequest([FromQuery] long requestId, [FromQuery] bool decision)
    {
        return Ok(await _employmentService.AnswereEmploymentRequest(requestId, decision));
    }

}
