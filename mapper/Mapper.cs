namespace BookingApp;

public static class Mapper
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
    public static FacilityDto MapToDto(this Facility facility){
        FacilityDto dto = new FacilityDto(
            facility.Id,
            facility.Name
        );
        return dto;
    }
    public static Facility MapToEntity(this FacilityCreateDto dto, Owner owner, Adress adress){
        Facility facility = new (){
            Name = dto.Name,
            Adress = adress,
            OwnerId = owner.Id,
            Owner = owner
        };
        return facility;
    }
}
