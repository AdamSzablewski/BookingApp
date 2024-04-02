namespace BookingApp;

public class Appointment
{
    public long Id {get; set;}
    public long ServiceId {get; set;}
    public Service Service {get; set;}

    public DateTime StartTime {get; set;}
    public DateTime EndTime {get; set;}

    public long CustomerId {get; set;}
    public Customer Customer {get; set;}



}
