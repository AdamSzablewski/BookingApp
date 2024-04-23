namespace BookingApp;

public static class MessageMapper
{
    public static MessageDto MapToDto(this Message message)
    {
        return new MessageDto()
        {
            Id = message.Id,
            Text = message.Text,
            Sender = message.Sender.MapToDto(),
            Viewers = message.Viewers.Select(v => v.Person.MapToDto()).ToList() ?? new List<PersonDto?>()
        };
    }
}
