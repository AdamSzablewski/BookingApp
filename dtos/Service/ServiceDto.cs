namespace BookingApp;

public class ServiceDto
{
    public long Id {get; set;}
    public string Name {get; set;}
    public decimal Price {get; set;}
    public TimeSpan Duration {get; set;}
    public List<EmployeeDto> Employees {get; set;}=[];
}
