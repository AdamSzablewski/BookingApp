namespace BookingApp;

public class Message
{
    public Message()
    {
        Viewers = [];
    }
    public long Id {get; set;}
    public string SenderId {get; set;}
    public required string Text {get; set;}
    public List<Person> Viewers {get; set;}

}
