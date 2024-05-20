using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp;
[ApiController]
[Authorize]
[Route("message")]
public class MessagingController(MessagingService messagingService) : ControllerBase
{
    private readonly MessagingService _messagingService = messagingService;

    [HttpPost]
    public async Task<IActionResult> CreateMessage([FromBody] MessageCreateDto messageCreateDto)
    {
        var message = await _messagingService.CreateMessage(messageCreateDto);
        if(message == null)
        {
            return BadRequest("Message could not be created");
        }
        return Ok(message);
    }
}
