namespace BookingApp;
using Microsoft.EntityFrameworkCore;

public class PersonRepository : IPersonRepository
{
    private readonly BookingAppContext _context;
    public PersonRepository(BookingAppContext context)
    {
        _context = context;
    }

    public Person createPerson(Person person)
    {
        _context.Add(person);
        _context.SaveChanges();
        return person;
    }

    public Person? deletePerson(long id)
    {
        throw new NotImplementedException();
    }

    public Person? getByEmail(string email)
    {
        return _context.persons.FirstOrDefault(p => p.Email == email);
    }

    public Person? getById(long id)
    {
        return _context.persons
            .Include(p => p.Owner)
            .Include(p => p.Employee)
            .Include(p => p.Customer)
            .FirstOrDefault(p => p.Id == id);
    }

    public Person? updatePerson(long id, Person person)
    {
        throw new NotImplementedException();
    }
}
