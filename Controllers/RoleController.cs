using Microsoft.AspNetCore.Mvc;

namespace BookingApp;
[ApiController]
[Route("role")]
public class RoleController : ControllerBase
{
    private readonly BookingAppContext _dbContext;
    private readonly CustomerService _customerService;


    public RoleController(BookingAppContext dbContext, CustomerService customerService){
        _dbContext = dbContext;
        _customerService = customerService;
    }
    [HttpGet("owner/{userId}")]
    public IActionResult AddOwnerFunctionality([FromRoute] string userId){
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

    [HttpGet("customer/{userId}")]
    public IActionResult AddCustomerFunctionality([FromRoute] string userId){
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
    [HttpGet("employee/{userId}")]
    public async Task<IActionResult> AddEmployeeFunctionality([FromRoute] string userId){
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
