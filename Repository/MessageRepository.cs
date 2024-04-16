
namespace BookingApp;

public class MessageRepository(BookingAppContext dbContext) : Repository<Message, long>(dbContext)
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
