namespace BookingApp;

public interface IFacilityRepository
{
    public Facility? GetById(long id);
    public Facility Create(Facility facility, Owner owner);
}
