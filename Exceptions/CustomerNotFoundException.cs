namespace BookingApp;

public class CustomerNotFoundException : Exception
{
    public CustomerNotFoundException()
    {

    }
    
    public CustomerNotFoundException(string message)
    : base(message)
    {
       
    }
}
