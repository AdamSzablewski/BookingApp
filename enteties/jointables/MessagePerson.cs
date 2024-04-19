namespace BookingApp;

public class MessagePerson
{
    public long Id {get; set;}
    public long MessageId {get; set;}
    public string PersonId {get; set;}
    public required Message Message {get; set;}
    public required Person Person {get; set;}

}
