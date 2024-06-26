﻿namespace BookingApp;

public class FacilityService(
    IFacilityRepository facilityRepository,
    IPersonRepository personRepository,
    DbContext dbContext,
    IAdressRepository adressRepository)
{
    private readonly IFacilityRepository _facilityRepository = facilityRepository;
    private readonly IPersonRepository _personRepository = personRepository;
    private readonly DbContext _dbContext = dbContext;
    private readonly IAdressRepository _adressRepository = adressRepository;

    public async Task<Facility> GetById(long id){
        Facility facility = await _facilityRepository.GetByIdAsync(id) ?? throw new FacilityNotFoundException();
        return facility;
    }

    public async Task<Facility> CreateAsync(string userId, FacilityCreateDto dto){
        Person person = await _personRepository.GetByIdAsync(userId) ??  throw new Exception("User not found");
        Owner owner = person.Owner ?? throw new Exception("user is not an owner");
        bool hasValidCategory = CategoryManager.GetCategoriesForServices().Any(c => c.Equals(dto.Category));
        if(!hasValidCategory)
        {
            dto.Category = "Other";
        }
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
            EndTime = new TimeOnly(17,0),
            Category = dto.Category
        };
        await _facilityRepository.CreateAsync(facility); 
        owner.Facilities.Add(facility);
        await _facilityRepository.UpdateAsync();
        return facility;
    }
    public async Task<Facility> ChangeName(long facilityId, FacilityChangeNameDto facilityChangeNameDto){
        Facility facility = await _facilityRepository.GetByIdAsync(facilityId) ?? throw new Exception("Facility not found");
        facility.Name = facilityChangeNameDto.Name;
        await _facilityRepository.UpdateAsync();
        return facility;
    }

    internal async Task<List<FacilityDto>>? GetForUser(string userId)
    {
        Person user = await _personRepository.GetByIdAsync(userId);
        if(user == null || user.Owner == null)
        {
            return null;
        }
        return user.Owner.Facilities.MapToDto();
    }

    internal async Task<List<FacilityDto>> GetFacilitiesByCriteria(string country, string city, string category)
    {
        List<Facility> facilities = await _facilityRepository.GetFacilitiesByCriteria(country, city, category, 50);
        return facilities.MapToDto();
    }
}
