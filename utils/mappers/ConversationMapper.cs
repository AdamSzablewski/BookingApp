using System.Linq;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
namespace BookingApp;

public static class ConversationMapper
{
    public static ConversationDto MapToDto(this Conversation conversation)
    {
        
        return new ConversationDto()
        {
            Id = conversation.Id,
            Participants = conversation.Participants.Select(p => p.Person.MapToDto()).ToList() ?? [],
            Messages = conversation.Messages.Select(m => m.MapToDto()).ToList() ?? []
        };
    }
}
