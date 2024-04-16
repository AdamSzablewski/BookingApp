namespace BookingApp;

public class Conversation
{
    public Conversation(){
        Messages = [];
        Participants = [];
    }
    public long Id {get; set;}
    public List<Message> Messages {get; set;}
    public required List<Person> Participants {get; set;}

}
