using Microsoft.AspNetCore.Mvc;

namespace BookingApp;
[ApiController]
[Route("conversation")]
public class ConversationController : ControllerBase
{
    private readonly ConversationService _conversationService;
    public ConversationController(ConversationService conversationService)
    {
        _conversationService = conversationService;
    }
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
    public Task<IActionResult> CreateConversation([FromBody]ConversationCreateDto conversationCreateDto)
    {
        return Ok(  _conversationService.CreateConversation(conversationCreateDto));
    }
    [HttpGet("{conversationId}/add")]
    public async Task<IActionResult> AddUserToConversation([FromQuery]string participantId, [FromQuery]string newParticipantId, [FromRoute] long conversationId)
    {
        return Ok(await _conversationService.AddUserToConversation(participantId, newParticipantId, conversationId));
    }
    [HttpGet("{conversationId}/leave")]
    public async Task<IActionResult> LeaveConversation([FromQuery]string participantId, [FromQuery]long conversationId)
    {
        return Ok(await _conversationService.LeaveConversation(participantId, conversationId));
    }
    

}
