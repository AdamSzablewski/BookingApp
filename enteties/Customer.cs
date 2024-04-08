namespace BookingApp;

public class Customer
{
    public Customer(){
        Appointments = [];
    }
    public long Id {get; set;}
    public long UserId {get; set;}
    public required Person User {get; set;}
    public List<Appointment> Appointments {get; set;}

}
