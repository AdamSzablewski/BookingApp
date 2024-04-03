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
    public async Task<FacilityDto> GetById(long id){
        Facility? facility = await _facilityRepository.GetByIdAsync(id);
        if(facility == null){
            throw new Exception("No such facility found");
        }
        return facility.MapToDto();
    }

    public async Task<Facility> CreateAsync(long userId, FacilityCreateDto dto){
        Person? person = await _personRepository.GetByIdAsync(userId);
        if(person == null){
            throw new Exception("User not found");
        }
        Owner? owner = person.Owner;
        if(owner == null){
            throw new Exception("user is not an owner");
        }
        Adress adress = new Adress(dto.Country, dto.City, dto.Street, dto.HouseNumber);
        Facility facility = await _facilityRepository.CreateAsync(dto.MapToEntity(owner, adress), owner);
        return facility;
    }
    public async Task<Facility> ChangeName(long facilityId, FacilityChangeNameDto facilityChangeNameDto){
        Facility? facility = await _facilityRepository.GetByIdAsync(facilityId);
        if(facility == null){
            throw new Exception("Facility not found");
        }
        facility.Name = facilityChangeNameDto.Name;
        await _facilityRepository.SaveAsync(facility);
        return facility;
    }
}
