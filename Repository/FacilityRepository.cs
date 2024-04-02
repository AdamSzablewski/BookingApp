namespace BookingApp;

public class FacilityRepository : IFacilityRepository
{
    private readonly BookingAppContext _context;
    public FacilityRepository(BookingAppContext context)
    {
        _context = context;
    }

    public Facility Create(Facility facility, Owner owner)
    {
        Adress adress = facility.Adress;
        _context.Add(facility);
        _context.Add(adress);
        owner.Facility = facility;
        _context.SaveChanges();
        return facility;
    }

    public Facility? GetById(long id)
    {
        throw new NotImplementedException();
    }
}
