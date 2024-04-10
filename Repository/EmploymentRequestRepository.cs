
using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class EmploymentRequestRepository : Repository<EmploymentRequest>
{
    public EmploymentRequestRepository(BookingAppContext dbContext) : base(dbContext)
    {
    }

    public override EmploymentRequest? GetById(long Id)
    {
         return _dbContext.EmploymentRequests
        .Include(e => e.Sender)
        .Include(e => e.Receiver)
        .Include(e => e.Facility)
        .FirstOrDefault(e => e.Id == Id);
    }

    public async override Task<EmploymentRequest?> GetByIdAsync(long Id)
    {
         return await _dbContext.EmploymentRequests
        .Include(e => e.Sender)
        .Include(e => e.Receiver)
        .Include(e => e.Facility)
        .FirstOrDefaultAsync(e => e.Id == Id);
    }
}
