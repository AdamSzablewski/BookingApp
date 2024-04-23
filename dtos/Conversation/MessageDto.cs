namespace BookingApp;

public class MessageDto
{
    public required long Id {get; set;}
    public required PersonDto Sender {get; set;}
    public required string Text {get; set;}
    public required List<PersonDto> Viewers {get; set;} = [];
}
