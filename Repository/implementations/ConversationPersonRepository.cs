﻿
namespace BookingApp;

public class ConversationPersonRepository(BookingAppContext dbContext) : Repository<ConversationPerson, long>(dbContext)
{
    public override ConversationPerson? GetById(long Id)
    {
        throw new NotImplementedException();
    }

    public override Task<ConversationPerson?> GetByIdAsync(long Id)
    {
        throw new NotImplementedException();
    }
}
