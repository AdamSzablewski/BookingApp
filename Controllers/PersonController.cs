using System.Data.Common;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp;
[ApiController]
[Route("user")]
public class PersonController : ControllerBase
{
    private readonly BookingAppContext _context;
    private readonly PersonService _personService;
    public PersonController(BookingAppContext dbContext, PersonService personService)
    {
        _context = dbContext;
        _personService = personService;
    }
    [HttpGet]
    public IActionResult getAll()
    {
       return Ok( _context.Persons.ToList()); 
    }

    [HttpGet("{id}")]
    public IActionResult getPersonById([FromRoute] string id)
    {
       return Ok( _personService.GetUserById(id)); 
    }
    [HttpPost]
    public IActionResult createPerson([FromBody] PersonCreateDto personCreateDto)
    {
        _personService.CreatePerson(personCreateDto);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> deletePerson([FromRoute] string id)
    {
       bool success = await _personService.DeletePerson(id); 
       if(success){
            return Ok();
       }else{
            return NotFound();
       }
       
    }
}
