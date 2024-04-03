
using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly BookingAppContext _dbContext;
    public AppointmentRepository(BookingAppContext dbContext){
        _dbContext = dbContext;
    }
    public async Task<Appointment> CreateAsync(Appointment appointment)
    {
        await _dbContext.AddAsync(appointment);
        await _dbContext.SaveChangesAsync();
        return appointment;
    }

    public async Task<Appointment?> DeleteAsync(long Id)
    {
        throw new NotImplementedException();
    }

    public async Task<Appointment?> GetByIdAsync(long Id)
    {
        return await _dbContext.Appointments.FirstOrDefaultAsync(e => e.Id == Id);
    }

    public async Task<Appointment?> UpdateAsync(Appointment appointment)
    {
        await _dbContext.SaveChangesAsync();
        return appointment;
    }
}
