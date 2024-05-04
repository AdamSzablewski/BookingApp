﻿
using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class ServiceRepository(BookingAppContext dbContext) : Repository<Service, long>(dbContext)
{
    public async Task<List<Service>> GetAllForFacility(long FacilityId)
    {
        return await _dbContext.Services
                    .Where(e => e.FacilityId == FacilityId)
                    .ToListAsync();
    }
    public async Task<List<Service>> GetInArea(string country, string city, int FEED_AMOUNT)
    {
        return await _dbContext.Services
            .Where(service => service.Facility.Adress.Country.Equals(country) && service.Facility.Adress.City.Equals(city))
            .Take(FEED_AMOUNT)
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
