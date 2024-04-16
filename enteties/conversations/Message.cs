namespace BookingApp;

public class Message
{
    public long Id {get; set;}
    public required string SenderId {get; set;}
    public string Text {get; set;}
    public List<Person> Viewers {get; set;}

}
