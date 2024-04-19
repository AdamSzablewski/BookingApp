namespace BookingApp;

public class OwnerNotFoundException : Exception
{
    public OwnerNotFoundException()
    {

    }
    
    public OwnerNotFoundException(string message)
    : base(message)
    {
       
    }

}
