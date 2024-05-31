

namespace BookingApp;

public interface IFacilityRepository : IRepository<Facility, long>
{
    Task<List<Facility>> GetInArea(string country, string city, int fEED_AMOUNT);
    Task<List<Facility>> GetInCountry(string country, int amount, int fEED_AMOUNT);
}
