namespace BookingApp;

public static class ConversationUtil
{
    public static bool CheckIfMember(this Conversation conversation, string userId)
    {
        return conversation.Participants.Any(p => p.Id.Equals(userId));
    }
}
