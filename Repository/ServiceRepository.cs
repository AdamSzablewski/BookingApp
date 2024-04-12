
using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class ServiceRepository : Repository<Service>
{
    public ServiceRepository(BookingAppContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Service>> GetAllForFacility(long FacilityId)
    {
        return await _dbContext.Services
                    .Where(e => e.FacilityId == FacilityId)
                    .ToListAsync();
    }

    public override Service? GetById(long Id)
    {
        return _dbContext.Services
        .Include(e => e.Employees)
        .Include(e => e.Facility)
        .FirstOrDefault(s => s.Id == Id);
    }

    public async override Task<Service?> GetByIdAsync(long Id)
    {
        return await _dbContext.Services
        .Include(e => e.Employees)
            .ThenInclude(e => e.Appointments)
        .Include(e => e.Facility)
        .FirstOrDefaultAsync(s => s.Id == Id);
    }
}
