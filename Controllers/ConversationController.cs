using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp;
[ApiController]
[Authorize]
[Route("conversation")]
public class ConversationController(ConversationService conversationService, SecurityService securityService) : ControllerBase
{
    private readonly ConversationService _conversationService = conversationService;
    private readonly SecurityService _securityService = securityService;

    [HttpGet("{conversationId}")]
    public async Task<IActionResult> GetConversationById([FromRoute]long conversationId)
    {
        var conversation = await _conversationService.GetConversationById(conversationId);
        if(conversation == null)
        {
            return NotFound("Conversation not found");
        }
        bool isMemebr = _securityService.IsMemberOfConversation(HttpContext, conversation);
        if(!isMemebr)
        {
            return Unauthorized("You can not access this conversation");
        }
        return Ok(conversation);
    }


    [HttpGet]
    public async Task<IActionResult> GetAllConversationsForUser([FromQuery]string userId)
    {
        
        return Ok(await _conversationService.GetConversationsForUser(userId));
    }
    [HttpPost]
    public async Task<IActionResult> CreateConversation([FromBody]ConversationCreateDto conversationCreateDto)
    {
        if(!ModelState.IsValid){ return BadRequest(ModelState);};
        var conversation = await _conversationService.CreateConversation(conversationCreateDto);
        return CreatedAtAction(nameof(GetConversationById), new{conversationId = conversation.Id}, conversation); 
    }
    [HttpGet("{conversationId}/add")]
    public async Task<IActionResult> AddUserToConversation([FromQuery]string participantId, [FromQuery]string newParticipantId, [FromRoute] long conversationId)
    {
        var conversation = await _conversationService.AddUserToConversation(participantId, newParticipantId, conversationId);
        if(conversation == null)
        {
            return BadRequest("Could not add the user to the conversation");
        }
        return Ok(conversation);
    }
    [HttpGet("{conversationId}/leave")]
    public async Task<IActionResult> LeaveConversation([FromQuery]string participantId, [FromRoute]long conversationId)
    {
        var conversation = await _conversationService.LeaveConversation(participantId, conversationId);
        if(conversation == null)
        {
            return BadRequest("Could not leave the conversation");
        }
        return Ok();
    }
    

}
