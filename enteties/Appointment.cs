namespace BookingApp;

public class Appointment
{
    public long Id {get; set;}
    public long ServiceId {get; set;}
    public required Service Service {get; set;}

    public required DateTime StartTime {get; set;}
    public required DateTime EndTime {get; set;}

    public long CustomerId {get; set;}
    public required Customer Customer {get; set;}
    public long EmployeeId {get; set;}
    public required Employee Employee {get; set;}




}
