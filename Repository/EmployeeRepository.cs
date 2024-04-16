
using System.Reflection.Metadata.Ecma335;

namespace BookingApp;

public class EmployeeRepository(BookingAppContext dbContext) : Repository<Employee, long>(dbContext)
{
    public override Employee? GetById(long Id)
    {
        return _dbContext.Employees.Find(Id);
    }

    public override async Task<Employee?> GetByIdAsync(long Id)
    {
        return await _dbContext.Employees.FindAsync(Id);
    }
}
