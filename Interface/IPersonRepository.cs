namespace BookingApp;

public interface IPersonRepository
{
    Person? getById(long id);
    Person createPerson(Person person);
    Person? updatePerson(long id, Person person);
    Person? deletePerson(long id);
    Person? getByEmail(string email);
}
