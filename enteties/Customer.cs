namespace BookingApp;

public class Customer
{
    public long Id {get; set;}
    public long? UserId {get; set;}
    public Person? User {get; set;}
    public List<Appointment> appointments {get; set;}

}
