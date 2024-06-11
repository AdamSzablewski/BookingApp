
namespace BookingApp;

public interface IAppointmentRepository : IRepository<Appointment, long>
{
    Task<List<Appointment>> GetForUser(string userId);
}
