
using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class FeedService(IPersonRepository personRepository, BookingAppContext dbContext, IServiceRepository serviceRepository)
{
    private static readonly int FEED_AMOUNT = 40;
    private readonly IPersonRepository _personRepository = personRepository;
    private readonly BookingAppContext _dbContext = dbContext;
    private readonly IServiceRepository _serviceRepositopry = serviceRepository;
    internal async Task<Feed> GetFeed(string userId)
    {
        Person user = await _personRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException();
        string Country = user.Adress.Country;
        string City = user.Adress.City;
        List<Service> servicesInArea = await _serviceRepositopry.GetInArea(Country, City, FEED_AMOUNT);
         await _dbContext.Services
            .Where(service => service.Facility.Adress.Country.Equals(Country) && service.Facility.Adress.City.Equals(City))
            .Take(FEED_AMOUNT)
            .ToListAsync();
            
        List<ServiceDto> serviceDtos = servicesInArea.Select(s => s.MapToDto()).ToList();
        Feed feed = new(){
            UserId = userId,
            Services = serviceDtos,
            page = 0
        };
        return feed;
                             
    }
}
