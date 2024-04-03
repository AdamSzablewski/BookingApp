namespace BookingApp;

public interface IFacilityRepository
{
    public Task<Facility?> GetByIdAsync(long id);
    public Task<Facility> CreateAsync(Facility facility, Owner owner);
    public Task<Facility> SaveAsync(Facility facility);
}
