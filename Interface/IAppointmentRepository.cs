namespace BookingApp;

public interface IAppointmentRepository
{
    Task<Appointment?> GetByIdAsync(long Id);
    Task<Appointment?> UpdateAsync(Appointment appointment);
    Task<Appointment> CreateAsync(Appointment appointment);
    Task<Appointment?> DeleteAsync(long id);
}
