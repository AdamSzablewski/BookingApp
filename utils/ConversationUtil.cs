namespace BookingApp;

public static class ConversationUtil
{
    public static bool IsMember(this Conversation conversation, string userId)
    {
         return conversation.Participants.Any(p => p.PersonId.Equals(userId));

    }
}
