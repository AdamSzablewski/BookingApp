namespace BookingApp;

public static class MessageUtil
{
    public static bool IsOwner(this Message message, string userId)
    {
        return message.SenderId.Equals(userId);
    }
}
