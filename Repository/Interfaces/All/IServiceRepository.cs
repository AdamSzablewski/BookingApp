namespace BookingApp;

public interface IServiceRepository : IRepository<Service, long>
{
    public Task<List<Service>> GetInArea(string country, string city, int amount);
    public Task<List<Service>> GetAllForFacility(long FacilityId);
    public Task<List<Service>> GetInCountry(string country, int amount);
}
