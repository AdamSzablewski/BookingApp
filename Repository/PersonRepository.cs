namespace BookingApp;
using Microsoft.EntityFrameworkCore;

public class PersonRepository : IPersonRepository
{
    private readonly BookingAppContext _context;
    public PersonRepository(BookingAppContext context)
    {
        _context = context;
    }

    public async Task<Person> CreatePersonAsync(Person person)
    {
        await _context.AddAsync(person);
        await _context.SaveChangesAsync();
        return person;
    }

    public Task<Person?> DeletePersonAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<Person?> GetByEmailAsync(string email)
    {
        return await _context.Persons.FirstOrDefaultAsync(p => p.Email == email);
    }

    public async Task<Person?> GetByIdAsync(long id)
    {
        return await _context.Persons
            .Include(p => p.Owner)
            .Include(p => p.Employee)
            .Include(p => p.Customer)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<Person?> UpdatePersonAsync(long id, Person person)
    {
        throw new NotImplementedException();
    }


}
