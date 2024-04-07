
namespace BookingApp;

public class EmploymentRequestRepository : IEmploymentRequestRepository
{
    private readonly BookingAppContext _dbContext;
    public EmploymentRequestRepository(BookingAppContext dbContext){
        _dbContext = dbContext;
    }
    public async Task<EmploymentRequest> Create(EmploymentRequest employmentRequest)
    {
        await _dbContext.AddAsync(employmentRequest);
        await _dbContext.SaveChangesAsync();
        return employmentRequest;
    }

    public Task<EmploymentRequest?> Delete(long Id)
    {
        throw new NotImplementedException();
    }

    public async Task<EmploymentRequest?> GetById(long Id)
    {
        return await _dbContext.EmploymentRequests.FindAsync(Id);
    }

    public Task<EmploymentRequest?> Update(EmploymentRequest employmentRequest)
    {
        throw new NotImplementedException();
    }
}
