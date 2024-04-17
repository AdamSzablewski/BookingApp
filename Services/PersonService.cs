
namespace BookingApp;

public class PersonService(PersonRepository repository)


{
    private readonly PersonRepository _personRepository = repository;

    public async Task<Person?> GetUserById(string id)
    {
     return await _personRepository.GetByIdAsync(id);
    }
    public async Task<Person?> GetUserByEmail(string email)
    {
     return await _personRepository.GetByEmailAsync(email);
    }
    public async Task<bool> DeletePerson(string id){
        Person user = await _personRepository.GetByIdAsync(id) ?? throw new Exception("User not found");
        _personRepository.Delete(user);
        return true;
    }
    public Person CreatePerson(PersonCreateDto personCreateDto){
        Person user = personCreateDto.MapToEntity();
        _personRepository.Create(user);
        return user;
    }

    
}
