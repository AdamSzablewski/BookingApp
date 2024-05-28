namespace BookingApp;

public class Conversation
{

    public long Id {get; set;}
    public List<Message> Messages {get; set;} = [];
    public List<ConversationPerson> Participants {get; set;} = [];

    
}
