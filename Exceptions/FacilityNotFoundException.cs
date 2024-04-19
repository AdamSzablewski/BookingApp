namespace BookingApp;

public class FacilityNotFoundException : Exception
{
    public FacilityNotFoundException()
    {

    }
    
    public FacilityNotFoundException(string message)
    : base(message)
    {
       
    }
}
