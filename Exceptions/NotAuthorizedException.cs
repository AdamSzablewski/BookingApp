namespace BookingApp;

public class NotAuthorizedException : Exception
{
    public NotAuthorizedException()
    {

    }
    
    public NotAuthorizedException(string message)
    : base(message)
    {
       
    }
}
