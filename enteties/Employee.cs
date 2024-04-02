namespace BookingApp;

public class Employee
{
    public long Id {get; set;}
   
    public long WorkplaceId {get; set;}
    public Facility Workplace {get; set;}
     public long UserId {get; set;}
    public Person User {get; set;}
    public List<Appointment> appointments {get; set;}
    public List<EmployeeService> services {get; set;}

}
