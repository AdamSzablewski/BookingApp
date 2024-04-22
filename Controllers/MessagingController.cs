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
        return Ok(await _messagingService.CreateMessage(messageCreateDto));
    }
}
