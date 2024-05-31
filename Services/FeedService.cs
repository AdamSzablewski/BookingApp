
using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class FeedService(IPersonRepository personRepository, DbContext dbContext, IServiceRepository serviceRepository, IFacilityRepository facilityRepository)
{
    private static readonly int FEED_AMOUNT = 40;
    private readonly IPersonRepository _personRepository = personRepository;
    private readonly IFacilityRepository _facilityRepository = facilityRepository;
    private readonly DbContext _dbContext = dbContext;
    private readonly IServiceRepository _serviceRepositopry = serviceRepository;
    internal async Task<List<FacilityDto>> GetFeed(string userId)
    {
        Person user = await _personRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException();
        string Country = user.Adress.Country;
        string City = user.Adress.City;
        List<Facility> facilitiesInArea = await _facilityRepository.GetInArea(Country, City, FEED_AMOUNT);
        if(facilitiesInArea.Count < FEED_AMOUNT)
        {
            int amount = FEED_AMOUNT - facilitiesInArea.Count;
            facilitiesInArea.AddRange( await _facilityRepository.GetInCountry(Country, amount, FEED_AMOUNT));
        }
        List<FacilityDto> facilities = facilitiesInArea.Select(f => f.MapToDto()).ToList();
        
        return facilities;
                             
    }
}
