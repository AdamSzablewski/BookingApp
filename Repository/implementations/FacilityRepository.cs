using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class FacilityRepository(DbContext dbContext) : Repository<Facility, long>(dbContext), IFacilityRepository
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

    public async Task<List<Facility>> GetInArea(string country, string city, int FEED_AMOUNT)
    {
        return await _dbContext.Facilities
            .Include(facility => facility.Adress)
            .Where(facility => facility.Adress.Country.Equals(country) && facility.Adress.City.Equals(city))
            .Take(FEED_AMOUNT)
            .ToListAsync();
    }

    public async Task<List<Facility>> GetInCountry(string country, int amount, int FEED_AMOUNT)
    {
        return await _dbContext.Facilities
            .Include(facility => facility.Adress)
            .Where(facility => facility.Adress.Country.Equals(country))
            .Take(FEED_AMOUNT)
            .ToListAsync();
    }

}
