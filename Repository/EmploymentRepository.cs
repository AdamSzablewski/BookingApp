
using System.Data.Common;

namespace BookingApp;

public class EmploymentRepository : IEmploymentRepository
{
    private readonly BookingAppContext _dbContext;
    public EmploymentRepository(BookingAppContext dbContext){
        _dbContext = dbContext;
    }
    public async Task<EmploymentRequest> CreateAsync(EmploymentRequest employmentRequest)
    {
        await _dbContext.AddAsync(employmentRequest);
        await _dbContext.SaveChangesAsync();
        return employmentRequest;
    }

    public async Task<EmploymentRequest> SaveAsync(EmploymentRequest employmentRequest)
    {
        await _dbContext.SaveChangesAsync();
        return employmentRequest;
    }
}
