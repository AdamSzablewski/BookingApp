namespace BookingApp;

public class AppointmentNotFoundException : Exception
{
    public AppointmentNotFoundException()
    {

    }
    
    public AppointmentNotFoundException(string message)
    : base(message)
    {
       
    }
}
