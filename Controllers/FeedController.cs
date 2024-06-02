using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp;
[ApiController]
[Authorize]
[Route("feed")]
public class FeedController(SecurityService securityService, FeedService feedService) : ControllerBase
{
    private readonly SecurityService _securityService = securityService;
    private readonly FeedService _feedService = feedService;
    [HttpGet]
    public async Task<IActionResult> GetFeed([FromQuery] string userId)
    {
        Console.WriteLine(userId);
        _securityService.IsUser(HttpContext, userId);
        return Ok(await _feedService.GetFeed(userId));
    }
}
