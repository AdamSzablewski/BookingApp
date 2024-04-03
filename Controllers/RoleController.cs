using Microsoft.AspNetCore.Mvc;

namespace BookingApp;
[ApiController]
[Route("role")]
public class RoleController : ControllerBase
{
    private readonly BookingAppContext _dbContext;

    public RoleController(BookingAppContext dbContext){
        _dbContext = dbContext;
    }
    [HttpGet("owner/{Id}")]
    public IActionResult AddOwnerFunctionality([FromRoute] long Id){
        Person? person = _dbContext.Persons.Find(Id);
        if(person == null){
            return NotFound();
        }
        Owner owner = new (){
            UserId = Id,
            User = person
        };
        _dbContext.Add(owner);
        _dbContext.SaveChanges();

        person.Owner = owner;
         _dbContext.SaveChanges();
        return Ok();
    }

    [HttpGet("client/{Id}")]
    public IActionResult AddCustomerFunctionality([FromRoute] long Id){
        Person? person = _dbContext.Persons.Find(Id);
        if(person == null){
            return NotFound();
        }
        Customer customer = new (){
            UserId = Id,
            User = person
        };
        _dbContext.Add(customer);
        _dbContext.SaveChanges();

        person.Customer = customer;
        _dbContext.SaveChanges();
        return Ok();
    }
    [HttpGet("employee/{Id}")]
    public IActionResult AddEmployeeFunctionality([FromRoute] long Id){
        Person? person = _dbContext.Persons.Find(Id);
        if(person == null){
            return NotFound();
        }
        Employee employee = new (){
            UserId = Id,
            User = person
        };
        _dbContext.Add(employee);
        _dbContext.SaveChanges();

        person.Employee = employee;
        _dbContext.SaveChanges();
        return Ok();
    }
}
