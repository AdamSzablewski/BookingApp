namespace BookingApp;

public class FacilityService
{
    private readonly IFacilityRepository _facilityRepository;
    private readonly IPersonRepository _personRepository;
    private readonly BookingAppContext _dbContext;
    public FacilityService(IFacilityRepository facilityRepository, IPersonRepository personRepository, BookingAppContext dbContext){
        _facilityRepository = facilityRepository;
        _personRepository = personRepository;
        _dbContext = dbContext;

    }
    public FacilityDto GetById(long id){
        Facility? facility = _facilityRepository.GetById(id);
        if(facility == null){
            return null;
        }
        return facility.MapToDto();
    }

    public Facility Create(long userId, FacilityCreateDto dto){
        Person? person = _personRepository.getById(userId);
        if(person == null){
            throw new Exception("User not found");
        }
        Owner? owner = person.Owner;
        if(owner == null){
            throw new Exception("user is not an owner");
        }
        Adress adress = new Adress(dto.Country, dto.City, dto.Street, dto.HouseNumber);
        Facility facility = _facilityRepository.Create(dto.MapToEntity(owner, adress), owner);


        return facility;
    }
}
