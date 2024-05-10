namespace BookingApp;

public class Customer
{
    
    public long Id {get; set;}
    public string? UserId {get; set;}
    public required Person User {get; set;}
    public List<Appointment> Appointments {get; set;} = [];
    public List<Review> CompletedReviews {get; set;} = [];
    public List<Review> ActiveReviews {get; set;} = [];
}
