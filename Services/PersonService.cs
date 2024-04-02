
namespace BookingApp;

public class PersonService

{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository repository)
    {
        _personRepository = repository;
    }
    public Person? GetUserById(long id)
    {
     return _personRepository.getById(id);
    }
    public Person? GetUserByEmail(string email)
    {
     return _personRepository.getByEmail(email);
    }
    public bool DeletePerson(long id){
        _personRepository.deletePerson(id);
        return true;
    }
    public void CreatePerson(PersonCreateDto personCreateDto){
        Person user = personCreateDto.MapToEntity();
        _personRepository.createPerson(user);
    }
}
