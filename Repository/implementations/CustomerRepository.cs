
namespace BookingApp;

public class CustomerRepository(BookingAppContext dbContext) : Repository<Customer, long>(dbContext)
{
    public override Customer? GetById(long Id)
    {
        return _dbContext.Customers.Find(Id);
    }

    public async override Task<Customer?> GetByIdAsync(long Id)
    {
        return await _dbContext.Customers.FindAsync(Id);
    }
}
