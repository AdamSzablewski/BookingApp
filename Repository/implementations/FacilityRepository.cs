using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class FacilityRepository(BookingAppContext dbContext) : Repository<Facility, long>(dbContext)
{
    public override Facility? GetById(long Id)
    {
        return _dbContext.Facilities
        .Include(f => f.Services)
            .ThenInclude(s => s.Employees)
        .FirstOrDefault(f => f.Id == Id);
    }
    public async override Task<Facility?> GetByIdAsync(long Id)
    {
        return await _dbContext.Facilities
        .Include(f => f.Services)
            .ThenInclude(s => s.Employees)
        .FirstOrDefaultAsync(f => f.Id == Id);
    }
}
