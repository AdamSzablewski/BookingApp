
using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class AppointmentRepository : Repository<Appointment, long>
{
    public AppointmentRepository(BookingAppContext dbContext) : base(dbContext)
    {
    }

    public override Appointment? GetById(long Id)
    {
        return _dbContext.Appointments.Find(Id);
    }

    public override async Task<Appointment?> GetByIdAsync(long Id)
    {
        return await _dbContext.Appointments.FindAsync(Id);
    }
}
