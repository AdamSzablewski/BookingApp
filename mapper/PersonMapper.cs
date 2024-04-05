namespace BookingApp;

public static class PersonMapper
{
    public static Person MapToEntity (this PersonCreateDto personDto){
       Person user = new(){
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                Email = personDto.Email,
                PhoneNumber = personDto.PhoneNumber,
                Password = personDto.Password
            }; 
        return user;     
    }
     public static Person MapToEntity (this PersonDto personDto){
       Person user = new(){
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                Email = personDto.Email,
                PhoneNumber = personDto.PhoneNumber,
            }; 
        return user;     
    }
    public static PersonDto MapToDto(this Person person, long id){
       PersonDto dto = new PersonDto(
                id,
                person.FirstName,
                person.LastName,
                person.Email,
                person.PhoneNumber
       ); 
        return dto;     
    }
    public static PersonDto MapToDto(this Person person){
       PersonDto dto = new PersonDto(
                person.Id,
                person.FirstName,
                person.LastName,
                person.Email,
                person.PhoneNumber
       ); 
        return dto;     
    }
}
