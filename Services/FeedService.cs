
using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class FeedService(PersonRepository personRepository, BookingAppContext dbContext)
{
    private static readonly int FEED_AMOUNT = 40;
    private readonly PersonRepository _personRepository = personRepository;
    private readonly BookingAppContext _dbContext = dbContext;
    internal async Task<Feed> GetFeed(string userId)
    {
        Person user = await _personRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException();
        string Country = user.Adress.Country;
        string City = user.Adress.City;
        List<Service> servicesInArea = await _dbContext.Services
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
