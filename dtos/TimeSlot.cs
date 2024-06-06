namespace BookingApp;

public class TimeSlot
{
    public List<EmployeeDto> Employees {get; set;}
    public TimeOnly StartTime  {get; set;}
    public TimeOnly EndTime  {get; set;}
    public DateOnly Date  {get; set;}
};
