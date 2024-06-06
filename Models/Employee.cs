namespace BookingApp;

public class Employee
{
    
    public long Id {get; set;}
    
    public TimeOnly StartTime {get; set;}
    public TimeOnly EndTime {get; set;}
    public long? WorkplaceId {get; set;}
    public Facility? Workplace {get; set;}
    public string? UserId {get; set;}
    public required Person User {get; set;}
    public List<Appointment> Appointments {get; set;} = [];
    public List<EmployeeService> Services {get; set;} = [];
    public List<EmploymentRequest> EmploymentRequests {get; set;} = [];

    public override string ToString()
        {
            // Create a string representation of the Employee object
            return $"Employee Id: {Id}, ";
        }
}
