namespace BookingApp;

public class FacilityRepository : IFacilityRepository
{
    private readonly BookingAppContext _context;
    public FacilityRepository(BookingAppContext context)
    {
        _context = context;
    }

    public async Task<Facility> CreateAsync(Facility facility, Owner owner)
    {
        Adress adress = facility.Adress;
        await _context.AddAsync(facility);
        await _context.AddAsync(adress);
        owner.Facility = facility;
        await _context.SaveChangesAsync();
        return facility;
    }

    public async Task<Facility?> GetByIdAsync(long Id)
    {
        return await _context.Facilities.FindAsync(Id);
    }
    public async Task<Facility> SaveAsync(Facility facility){
        _context.Facilities.Update(facility);
        await _context.SaveChangesAsync();
        return facility;
    }

   
}
