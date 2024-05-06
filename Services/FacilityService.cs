namespace BookingApp;

public class FacilityService(
    IFacilityRepository facilityRepository,
    IPersonRepository personRepository,
    BookingAppContext dbContext,
    IAdressRepository adressRepository)
{
    private readonly IFacilityRepository _facilityRepository = facilityRepository;
    private readonly IPersonRepository _personRepository = personRepository;
    private readonly BookingAppContext _dbContext = dbContext;
    private readonly IAdressRepository _adressRepository = adressRepository;

    public async Task<FacilityDto> GetById(long id){
        Facility? facility = await _facilityRepository.GetByIdAsync(id);
        if(facility == null){
            throw new Exception("No such facility found");
        }
        return facility.MapToDto();
    }

    public async Task<Facility> CreateAsync(string userId, FacilityCreateDto dto){
        Person person = await _personRepository.GetByIdAsync(userId) ??  throw new Exception("User not found");
        Owner owner = person.Owner ?? throw new Exception("user is not an owner");

        Adress adress = new(){
            Country = dto.Country,
            City = dto.City,
            Street = dto.Street,
            HouseNumber =  dto.HouseNumber
        };
        
        await _adressRepository.CreateAsync(adress);
        Facility facility = new()
        {
            Name = dto.Name,
            Adress = adress,
            Owner = owner,
            StartTime = new TimeOnly(9,0),
            EndTime = new TimeOnly(17,0)
        };
        try{
           await _facilityRepository.CreateAsync(facility); 
        }catch(Exception e){
            Console.WriteLine(e.StackTrace);
        }
        
        owner.Facilities.Add(facility);
        await _dbContext.SaveChangesAsync();
        return facility;
    }
    public async Task<Facility> ChangeName(long facilityId, FacilityChangeNameDto facilityChangeNameDto){
        Facility facility = await _facilityRepository.GetByIdAsync(facilityId) ?? throw new Exception("Facility not found");
        facility.Name = facilityChangeNameDto.Name;
        await _facilityRepository.UpdateAsync();
        return facility;
    }

}
