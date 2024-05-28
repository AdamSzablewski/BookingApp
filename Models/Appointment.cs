namespace BookingApp;

public class Appointment : IUserResource
{
    public Appointment()
    {

    }
    
    public long Id {get; set;}
    public long ServiceId {get; set;}
    public Service Service {get; set;}
    public DateTime StartTime {get; set;}
    public DateTime EndTime {get; set;}

    public long CustomerId {get; set;}
    public Customer Customer {get; set;}
    public long EmployeeId {get; set;}
    public Employee Employee {get; set;}
    public bool Completed {get; set;}

    public string? GetUserId()
    {
        return Customer?.UserId;
    }

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
