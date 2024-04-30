
namespace BookingApp;

public class PersonService(PersonRepository repository)


{
    private readonly PersonRepository _personRepository = repository;

    public async Task<PersonDto> GetUserDtoById(string id)
    {
     Person person =  await _personRepository.GetByIdAsync(id) ?? throw new UserNotFoundException();
     return person.MapToDto();
    }
    public async Task<PersonDto> GetUserByEmail(string email)
    {
     Person person =  await _personRepository.GetByEmailAsync(email) ?? throw new UserNotFoundException();
     return person.MapToDto();
    }
    public async Task<bool> DeletePerson(string id){
        Person user = await _personRepository.GetByIdAsync(id) ?? throw new Exception("User not found");
        bool success = await _personRepository.DeleteAsync(user);
        if(!success) throw new Exception("User not deleted");
        return true;
    }
    public async Task<PersonDto> UpdatePerson(PersonUpdateDto updateDto, string userId)
    {
        Person user = await _personRepository.GetByIdAsync(userId) ?? throw new Exception("User not found");
        user.Email = updateDto.Email;
        user.PhoneNumber = updateDto.PhoneNumber;
        user.FirstName = updateDto.FirstName;
        user.LastName = updateDto.LastName;
        
        await _personRepository.UpdateAsync();
        return user.MapToDto();
    }
    public async Task<PersonDto> UpdateEmail(PersonUpdateEmailDto personUpdateEmailDto, string userId)
    {
        Person user = await _personRepository.GetByIdAsync(userId) ?? throw new Exception("User not found");
        user.Email = personUpdateEmailDto.Email;
        
        await _personRepository.UpdateAsync();
        return user.MapToDto();
    }

    
}
