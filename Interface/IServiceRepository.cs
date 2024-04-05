namespace BookingApp;

public interface IServiceRepository
{
    Task<Service?> GetByIdAsync(long Id);
    Task<Service> CreateAsync(Service service);
    Task<Service> DeleteAsync(long Id);
    Task<Service?> UpdateAsync(Service service);
    Task<List<Service>> GetAllForFacility(long FacilityId);

}
