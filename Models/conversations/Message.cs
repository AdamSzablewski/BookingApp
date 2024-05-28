namespace BookingApp;

public class Message : IUserResource
{
    public long Id {get; set;}
    public string SenderId {get; set;}
    public Person Sender {get; set;}
    public required string Text {get; set;}
    public List<MessagePerson> Viewers {get; set;} = [];

    public string GetUserId()
    {
        return SenderId;
    }
}
