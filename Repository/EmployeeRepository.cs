
using System.Reflection.Metadata.Ecma335;

namespace BookingApp;

public class EmployeeRepository : IEmployeeRepository
{
    private BookingAppContext _dbContext;
    public EmployeeRepository(BookingAppContext dbContext){
        _dbContext = dbContext;
    }
    public async Task<Employee> Create(Employee employee)
    {
        await _dbContext.Employees.AddAsync(employee);
        await _dbContext.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> Delete(Employee employee)
    {
        _dbContext.Remove(employee);
        await _dbContext.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee?> GetById(long Id)
    {
        return await _dbContext.Employees.FindAsync(Id);
    }

    public async Task<Employee> Update(Employee employee)
    {
        await _dbContext.SaveChangesAsync();
        return employee;
    }
}
