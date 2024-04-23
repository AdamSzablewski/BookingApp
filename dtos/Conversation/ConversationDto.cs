namespace BookingApp;

public class ConversationDto
{
    public required long Id {get; set;}
    public required List<PersonDto> Participants {get; set;} = [];
    public required List<MessageDto> Messages {get; set;} = [];
}
