

namespace BookingApp;

public interface IFacilityRepository : IRepository<Facility, long>
{
    Task<List<Facility>> GetFacilitiesByCriteria(string country, string city, string serviceName, int FEED_AMOUNT);
    Task<List<Facility>> GetInArea(string country, string city, int fEED_AMOUNT);
    Task<List<Facility>> GetInCountry(string country, int amount, int fEED_AMOUNT);
}
