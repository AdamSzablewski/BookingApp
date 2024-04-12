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
   public override string ToString()
        {
            return $"Appointment ID: {Id}" + Environment.NewLine +
                   $"Service: {Service?.Name}" + Environment.NewLine +
                   $"Start Time: {StartTime}" + Environment.NewLine +
                   $"End Time: {EndTime}" + Environment.NewLine +
                   $"Customer: {Customer?.Id}" + Environment.NewLine +
                   $"Employee: {Employee?.Id}";
        }



}
