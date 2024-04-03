namespace BookingApp;

public interface IPersonRepository
{
    Task<Person?> GetByIdAsync(long id);
    Task<Person> CreatePersonAsync(Person person);
    Task<Person?> UpdatePersonAsync(long id, Person person);
    Task<Person?> DeletePersonAsync(long id);
    Task<Person?> GetByEmailAsync(string email);
}
