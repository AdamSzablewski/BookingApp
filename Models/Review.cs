namespace BookingApp;

public class Review
{
    public long Id {get; set;}
    public int Points {get; set;}
    public string Text {get; set;}
    public string UserId {get; set;}
    public Person User {get; set;}
    public bool IsActive {get; set;}
}
