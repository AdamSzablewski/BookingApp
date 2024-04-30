namespace BookingApp;

public static class PersonMapper
{
   
     public static Person MapToEntity (this PersonDto personDto)
     {
        if(personDto == null) return null;
        Person user = new(){
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                Email = personDto.Email,
                PhoneNumber = personDto.PhoneNumber,
            }; 
        return user;     
    }
    public static PersonDto? MapToDto(this Person person, string id)
    {
        if(person == null) return null;
        PersonDto dto = new PersonDto(
                id,
                person.FirstName,
                person.LastName,
                person.Email,
                person.PhoneNumber
       ); 
        return dto;     
    }
    public static PersonDto? MapToDto(this Person person)
    {
        if(person == null) return null;
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
