namespace BookingApp;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class PersonRepository(BookingAppContext dbContext) : Repository<Person, string>(dbContext), IPersonRepository
{
   
    public async Task<List<Person>> GetPeopleAsync(List<string> Ids)
    {
        return await _dbContext.Persons
        .Where(person => Ids.Contains(person.Id))
        .ToListAsync();
    }
    public async Task<List<Person>> GetPeople(List<string> Ids)
    {
        return await _dbContext.Persons
        .Where(person => Ids.Contains(person.Id))
        .ToListAsync();
    }

    public async Task<Person?> GetByEmailAsync(string email)
    {
        return await _dbContext.Persons.FirstOrDefaultAsync(p => p.Email == email);
    }

    public override Person? GetById(string Id)
    {
        return _dbContext.Persons.Find(Id);
    }

    public async override Task<Person?> GetByIdAsync(string Id)
    {
        return await _dbContext.Persons
            .Include(p => p.Owner)
                .ThenInclude(e => e.Facilities)
            .Include(p => p.Employee)
            .Include(p => p.Customer)
            .Include(p => p.Adress)
            .FirstOrDefaultAsync(p => p.Id.Equals( Id));
    }

}
