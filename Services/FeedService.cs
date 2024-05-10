
using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class FeedService(IPersonRepository personRepository, DbContext dbContext, IServiceRepository serviceRepository)
{
    private static readonly int FEED_AMOUNT = 40;
    private readonly IPersonRepository _personRepository = personRepository;
    private readonly DbContext _dbContext = dbContext;
    private readonly IServiceRepository _serviceRepositopry = serviceRepository;
    internal async Task<Feed> GetFeed(string userId)
    {
        Person user = await _personRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException();
        string Country = user.Adress.Country;
        string City = user.Adress.City;
        List<Service> servicesInArea = await _serviceRepositopry.GetInArea(Country, City, FEED_AMOUNT);
        if(servicesInArea.Count < FEED_AMOUNT)
        {
            int amount = FEED_AMOUNT - servicesInArea.Count;
            servicesInArea.AddRange( await _serviceRepositopry.GetInCountry(Country, amount));
        }
        List<ServiceDto> serviceDtos = servicesInArea.Select(s => s.MapToDto()).ToList();
        Feed feed = new(){
            UserId = userId,
            Services = serviceDtos,
            page = 0
        };
        return feed;
                             
    }
}
