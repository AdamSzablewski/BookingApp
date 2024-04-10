
using System.Data.Common;

namespace BookingApp;

public class EmploymentRepository : Repository<EmploymentRequest>
{
    public EmploymentRepository(BookingAppContext dbContext) : base(dbContext)
    {
    }

    public override EmploymentRequest? GetById(long Id)
    {
        return _dbContext.EmploymentRequests.Find(Id);
    }

    public override async Task<EmploymentRequest?> GetByIdAsync(long Id)
    {
        return await _dbContext.EmploymentRequests.FindAsync(Id);
    }
}
