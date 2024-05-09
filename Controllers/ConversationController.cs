using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp;
[ApiController]
[Authorize]
[Route("conversation")]
public class ConversationController(ConversationService conversationService) : ControllerBase
{
    private readonly ConversationService _conversationService = conversationService;

    [HttpGet("{conversationId}")]
    public async Task<IActionResult> GetConversationById([FromRoute]long conversationId)
    {
        return Ok(await _conversationService.GetConversationById(conversationId));
    }
    [HttpGet]
    public async Task<IActionResult> GetAllConversationsForUser([FromQuery]string Id)
    {
        return Ok(await _conversationService.GetConversationsForUser(Id));
    }
    [HttpPost]
    public async Task<IActionResult> CreateConversation([FromBody]ConversationCreateDto conversationCreateDto)
    {
        return Ok( await _conversationService.CreateConversation(conversationCreateDto));
    }
    [HttpGet("{conversationId}/add")]
    public async Task<IActionResult> AddUserToConversation([FromQuery]string participantId, [FromQuery]string newParticipantId, [FromRoute] long conversationId)
    {
        return Ok(await _conversationService.AddUserToConversation(participantId, newParticipantId, conversationId));
    }
    [HttpGet("{conversationId}/leave")]
    public async Task<IActionResult> LeaveConversation([FromQuery]string participantId, [FromRoute]long conversationId)
    {
        return Ok(await _conversationService.LeaveConversation(participantId, conversationId));
    }
    

}
