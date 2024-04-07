namespace BookingApp;

public interface IFacilityRepository
{
    public Task<Facility?> GetByIdAsync(long id);
    public Task<Facility> CreateAsync(Facility facility);
    public Task<Facility> SaveAsync(Facility facility);
}
