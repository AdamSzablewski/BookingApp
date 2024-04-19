namespace BookingApp;

public class MessagingService(MessageRepository messageRepository, ConversationRepository conversationRepository)
{
    private readonly MessageRepository _messageRepository = messageRepository;
    private readonly ConversationRepository _conversationRepository = conversationRepository;


    public async Task<Message> CreateMessage(MessageCreateDto messageCreateDto)
    {
        Conversation conversation = _conversationRepository.GetById(messageCreateDto.ConversationId) ?? throw new Exception("Conversation not found");
        bool isMember = conversation.CheckIfMember(messageCreateDto.SenderId);
        if(!isMember)
        {
            throw new Exception("Not member of conversation");
        }
        Message message = new()
        {
            Text = messageCreateDto.Message,
            SenderId = messageCreateDto.SenderId
        };
        _messageRepository.Create(message);
        conversation.Messages.Add(message);
        _conversationRepository.Update();
        return message;
    }
     public async Task<Message> DeleteMessage(long messageId, string userId)
    {
        Conversation conversation = await _conversationRepository.GetConversationByMessageId(messageId) ?? throw new Exception("Conversation Not found");
        Message message = conversation.Messages
            .FirstOrDefault(m => m.Id == messageId) ?? throw new Exception("Message Not found");
        message.IsOwner(userId);
        conversation.Messages.Remove(message);
        _messageRepository.Delete(message);
        return message;
    }
}
