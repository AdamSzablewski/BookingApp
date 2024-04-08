namespace BookingApp;

public class Service
{
    public Service() {
        Employees = [];
    }
    public long Id {get; set;}
    public required string Name {get; set;}
    public required decimal Price {get; set;}
    public List<Employee> Employees {get; set;}
    public long FacilityId {get; set;}
    public required Facility Facility {get; set;}
    public TimeSpan Length {get; set;}

}

