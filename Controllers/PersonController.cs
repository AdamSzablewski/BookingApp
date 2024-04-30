using System.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp;
[ApiController]
[Authorize]
[Route("user")]
public class PersonController : ControllerBase
{
    private readonly BookingAppContext _context;
    private readonly PersonService _personService;
    private readonly SecurityService _securityService;
    public PersonController(
        BookingAppContext dbContext,
        PersonService personService,
        SecurityService securityService)
    {
        _context = dbContext;
        _personService = personService;
        _securityService = securityService;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser([FromRoute] string id)
    {
       return Ok(await _personService.GetUserDtoById(id)); 
    }
    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser([FromRoute] string userId, [FromBody] PersonUpdateDto updateDto)
    {
         _securityService.IsUser(HttpContext, userId);
        return Ok(await _personService.UpdatePerson(updateDto, userId)); 
    }
    [HttpPatch("{userId}/email")]
    public async Task<IActionResult> UpdateUserEmail([FromRoute] string userId, [FromBody] PersonUpdateEmailDto personUpdateEmailDto)
    {
        if(!ModelState.IsValid){ return BadRequest(ModelState);};
        _securityService.IsUser(HttpContext, userId);
        return Ok( _personService.UpdateEmail(personUpdateEmailDto, userId));
        
    }
    
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser([FromRoute] string userId)
    {
        _securityService.IsUser(HttpContext, userId);
        return Ok( await _personService.DeletePerson(userId));
       
    }
}
