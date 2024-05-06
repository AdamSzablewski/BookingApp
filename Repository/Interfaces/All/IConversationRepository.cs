namespace BookingApp;

public interface IConversationRepository : IRepository<Conversation, long>
{
    public Task<List<Conversation>> GetAllForUser(string Id);
    public Task<Conversation> GetConversationByMessageId(long Id);
}
