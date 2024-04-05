namespace BookingApp;

public class Service
{
    public long Id {get; set;}
    public string Name {get; set;}
    public decimal Price {get; set;}
    public List<Employee> Employees {get; set;}
    public long FacilityId {get; set;}
    public Facility Facility {get; set;}
    public TimeSpan Length {get; set;}

}

