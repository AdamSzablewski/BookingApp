namespace BookingApp;
using Microsoft.EntityFrameworkCore;

public class PersonRepository : Repository<Person>
{
    public PersonRepository(BookingAppContext dbContext) : base(dbContext)
    {
    }

    public async Task<Person?> GetByEmailAsync(string email)
    {
        return await _dbContext.Persons.FirstOrDefaultAsync(p => p.Email == email);
    }

    public override Person? GetById(long Id)
    {
        return _dbContext.Persons.Find(Id);
    }

    public async override Task<Person?> GetByIdAsync(long Id)
    {
        return await _dbContext.Persons
            .Include(p => p.Owner)
                .ThenInclude(e => e.Facilities)
            .Include(p => p.Employee)
            .Include(p => p.Customer)
            .FirstOrDefaultAsync(p => p.Id == Id);
    }

}
