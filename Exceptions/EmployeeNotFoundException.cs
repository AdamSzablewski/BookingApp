namespace BookingApp;

public class EmployeeNotFoundException : Exception
{
    public EmployeeNotFoundException()
    {

    }
    
    public EmployeeNotFoundException(string message)
    : base(message)
    {
       
    }
}
