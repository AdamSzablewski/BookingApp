
using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class AdressRepository(DbContext dbContext) : Repository<Adress, long>(dbContext), IAdressRepository
{
    private readonly DbContext _dbContext = dbContext;
    public override Adress? GetById(long Id)
    {
        return  _dbContext.Adresses.Find(Id);
    }

    public override Task<Adress?> GetByIdAsync(long Id)
    {
        return _dbContext.Adresses.FirstOrDefaultAsync(e => e.Id == Id);
    }
}
