namespace BookingApp;

public interface IServiceRepository : IRepository<Service, long>
{
    public Task<List<Service>> GetInArea(string country, string city, int FEED_AMOUNT);
    public Task<List<Service>> GetAllForFacility(long FacilityId);
}
