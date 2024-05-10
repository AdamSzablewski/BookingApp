
namespace BookingApp;

public class MessageRepository(DbContext dbContext) : Repository<Message, long>(dbContext), IMessageRepository
{
    public override Message? GetById(long Id)
    {
        return _dbContext.Messages.Find(Id);
    }

    public override async Task<Message?> GetByIdAsync(long Id)
    {
        return await _dbContext.Messages.FindAsync(Id);
    }
}
