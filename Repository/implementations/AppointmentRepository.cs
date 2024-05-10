
using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class AppointmentRepository(DbContext dbContext) : Repository<Appointment, long>(dbContext), IAppointmentRepository
{
    public override Appointment? GetById(long Id)
    {
        return _dbContext.Appointments.Find(Id);
    }

    public override async Task<Appointment?> GetByIdAsync(long Id)
    {
        return await _dbContext.Appointments.FindAsync(Id);
    }
}
