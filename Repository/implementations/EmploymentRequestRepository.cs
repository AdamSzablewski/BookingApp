
using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class EmploymentRequestRepository(DbContext dbContext) : Repository<EmploymentRequest, long>(dbContext), IEmploymentRequestRepository
{
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
            .ThenInclude(e => e.User)
        .Include(e => e.Receiver)
            .ThenInclude(e => e.User)
        .Include(e => e.Facility)
        .FirstOrDefaultAsync(e => e.Id == Id);
    }
}
