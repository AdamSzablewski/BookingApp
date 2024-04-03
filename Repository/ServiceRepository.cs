
using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class ServiceRepository : IServiceRepository
{
    private readonly BookingAppContext _dbContext;
    public ServiceRepository(BookingAppContext dbContext){
        _dbContext = dbContext;
    }
    public async Task<Service> CreateAsync(Service service)
    {
        await _dbContext.AddAsync(service);
        await _dbContext.SaveChangesAsync();
        return service;
    }
    public Task<Service> DeleteAsync(long Id)
    {
        throw new NotImplementedException();
    }
    public async Task<Service?> GetByIdAsync(long Id)
    {
        return await _dbContext.Services.FirstOrDefaultAsync(s => s.Id == Id);
    }
    public async Task<Service?> UpdateAsync(Service service)
    {
        await _dbContext.SaveChangesAsync();
        return service;
    }
}
