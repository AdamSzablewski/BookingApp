
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

    public async Task<List<Appointment>> GetForUser(string userId)
    {
        return await _dbContext.Appointments
            .Include(a => a.Customer)
                .ThenInclude(c => c.User)
            .Include(a => a.Service)
                .ThenInclude(s => s.Facility)
                    .ThenInclude(f => f.Adress)
            .Where(a => a.Customer.UserId.Equals(userId))
            .ToListAsync();
    }
}
