using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class FacilityRepository(DbContext dbContext) : Repository<Facility, long>(dbContext), IFacilityRepository
{
    public override Facility? GetById(long Id)
    {
        return _dbContext.Facilities
        .Include(f => f.Services)
            .ThenInclude(s => s.Employees)
                .ThenInclude(e => e.User)
        .FirstOrDefault(f => f.Id == Id);
    }
    public async override Task<Facility?> GetByIdAsync(long Id)
    {
        return await _dbContext.Facilities
        .Include(f => f.Services)
            .ThenInclude(s => s.Employees)
                .ThenInclude(e => e.User)
        .FirstOrDefaultAsync(f => f.Id == Id);
    }

    public async Task<List<Facility>> GetFacilitiesByCriteria(string country, string city, string serviceName,int FEED_AMOUNT)
    {
        return await _dbContext.Facilities
        .Include(f => f.Services)
            .ThenInclude(s => s.Employees)
                .ThenInclude(e => e.User)
        .Where(f => f.Adress.Country.Equals(country))
        .Where(f => f.Adress.City.Equals(city))
        .Where(f => f.Services.Any(s => s.Name.Equals(serviceName)))
        .OrderBy(f  => PointsUtil.GetScore(f.Reviews))
        .Take(FEED_AMOUNT)
        .ToListAsync();
    }



    public async Task<List<Facility>> GetInArea(string country, string city, int FEED_AMOUNT)
    {
        return await _dbContext.Facilities
            .Include(facility => facility.Adress)
            .Include(facility => facility.Reviews)
            .Where(facility => facility.Adress.Country.Equals(country) && facility.Adress.City.Equals(city))
            .Take(FEED_AMOUNT)
            .ToListAsync();
    }

    public async Task<List<Facility>> GetInCountry(string country, int amount, int FEED_AMOUNT)
    {
        return await _dbContext.Facilities
            .Include(facility => facility.Adress)
            .Include(facility => facility.Reviews)
            .Where(facility => facility.Adress.Country.Equals(country))
            .Take(FEED_AMOUNT)
            .ToListAsync();
    }

}
