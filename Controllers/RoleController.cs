using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;

namespace BookingApp;
[ApiController]
[Route("role")]
public class RoleController : ControllerBase
{
    private readonly DbContext _dbContext;
    private readonly CustomerService _customerService;
    private readonly SecurityService _securityService;

    public RoleController(DbContext dbContext, CustomerService customerService, SecurityService securityService)
    {
        _dbContext = dbContext;
        _customerService = customerService;
        _securityService = securityService;
    }
    [HttpGet("owner")]
    public IActionResult AddOwnerFunctionality([FromQuery] string token){
        string userId = _securityService.GetUserIdFromToken(token);
        Person? person = _dbContext.Persons.Find(userId);
        if(person == null){
            return NotFound();
        }
        Owner owner = new (){
            UserId = userId,
            User = person
        };
        _dbContext.Owners.Add(owner);
        _dbContext.SaveChanges();

        person.Owner = owner;
        _dbContext.SaveChanges();
        return Ok();
    }

    [HttpGet("customer")]
    public IActionResult AddCustomerFunctionality([FromQuery] string token){
        string userId = _securityService.GetUserIdFromToken(token);
        Person? person = _dbContext.Persons.Find(userId);
        if(person == null){
            return NotFound();
        }
        Customer customer = new (){
            UserId = userId,
            User = person
        };
        _dbContext.Add(customer);
        _dbContext.SaveChanges();

        person.Customer = customer;
        _dbContext.SaveChanges();
        return Ok();
    }
    [HttpGet("employee")]
    public async Task<IActionResult> AddEmployeeFunctionality([FromQuery] string token){
        string userId = _securityService.GetUserIdFromToken(token);
        Person? person = _dbContext.Persons.Find(userId);
        if(person == null){
            return NotFound();
        }
        Employee employee = new (){
            UserId = userId,
            User = person,
            StartTime = new TimeOnly(9,0),
            EndTime = new TimeOnly(17,0),
        };

        _dbContext.Employees.Add(employee);
        _dbContext.SaveChanges();

        person.Employee = employee;
        _dbContext.SaveChanges();
        return Ok();
    }
}
