
namespace BookingApp;

public class PersonService

{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository repository)
    {
        _personRepository = repository;
    }
    public async Task<Person?> GetUserById(long id)
    {
     return await _personRepository.GetByIdAsync(id);
    }
    public async Task<Person?> GetUserByEmail(string email)
    {
     return await _personRepository.GetByEmailAsync(email);
    }
    public async Task<bool> DeletePerson(long id){
        await _personRepository.DeletePersonAsync(id);
        return true;
    }
    public void CreatePerson(PersonCreateDto personCreateDto){
        Person user = personCreateDto.MapToEntity();
        _personRepository.CreatePersonAsync(user);
    }
}
