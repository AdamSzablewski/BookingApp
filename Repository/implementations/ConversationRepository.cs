﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.VisualBasic;

namespace BookingApp;

public class ConversationRepository(DbContext dbContext) : Repository<Conversation, long>(dbContext), IConversationRepository
{
    public override Conversation? GetById(long Id)
    {
        return _dbContext.Conversations
        .Include(e => e.Participants)
        .FirstOrDefault(e => e.Id == Id);
    }

    public override async Task<Conversation?> GetByIdAsync(long Id)
    {
        return await _dbContext.Conversations
        .Include(e => e.Messages)
        .FirstOrDefaultAsync(e => e.Id.Equals(Id));
    }
    public async Task<List<Conversation>> GetAllForUser(string Id)
    {
        var conversations = from conv in _dbContext.Conversations
                            where conv.Participants.Any(p => p.Id.Equals(Id))
                            select conv;

        return await conversations.ToListAsync();
    }
    public async Task<Conversation> GetConversationByMessageId(long Id)
    {
        var conversation = from conv in _dbContext.Conversations
                            where conv.Messages.Any(m => m.Id.Equals(Id))
                            select conv;

        return  (Conversation) conversation;
    }
}
