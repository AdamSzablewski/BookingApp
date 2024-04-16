namespace BookingApp;

public class FacilityRepository(BookingAppContext dbContext) : Repository<Facility, long>(dbContext)
{
    public override Facility? GetById(long Id)
    {
         return _dbContext.Facilities.Find(Id);
    }
    public async override Task<Facility?> GetByIdAsync(long Id)
    {
        return await _dbContext.Facilities.FindAsync(Id);
    }
}
