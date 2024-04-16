namespace BookingApp;

public class ConversationService(ConversationRepository conversationRepository, PersonRepository personRepository)
{
    private readonly ConversationRepository _conversationRepository = conversationRepository;
    private readonly PersonRepository _personRepository = personRepository;

    public async Task<Conversation> GetConversationById(long id)
    {
        return await _conversationRepository.GetByIdAsync(id) ?? throw new Exception("Conversation not found");
    }
    public async Task<List<Conversation>> GetConversationsForUser(string id)
    {
        return await _conversationRepository.GetAllForUser(id) ?? throw new Exception("Conversation not found");
    }

    public async Task<Conversation> CreateConversation(ConversationCreateDto conversationCreateDto)
    {
        List<Person> users = await _personRepository.GetPeople(conversationCreateDto.Participants);
        Conversation conversation = new()
        {
            Participants = users
        };
        await _conversationRepository.CreateAsync(conversation);
        return conversation;
    }

    public async Task<Conversation> LeaveConversation(string personId, long conversationId)
    {
        Person user = await _personRepository.GetByIdAsync(personId) ?? throw new Exception("User not found");
        Conversation conversation = _conversationRepository.GetById(conversationId) ?? throw new Exception("Conversation not found");
        conversation.Participants.Remove(user);
        if(conversation.Participants.Count > 0)
        {
            _conversationRepository.Delete(conversation);
        }
        else
        {
            _conversationRepository.UpdateAsync();
        }
        
        return conversation;
    }
    public async Task<Conversation> AddUserToConversation(string participantId, string newParticipantId, long conversationId)
    {
        Conversation conversation = await _conversationRepository.GetByIdAsync(conversationId) ?? throw new Exception("Conversation not found");
        bool participantPresent = conversation.Participants.Any(p=> p.Id.Equals(participantId));
        if(!participantPresent)
        {
            throw new Exception("Participant not present");
        }
        Person newParticipant = await _personRepository.GetByIdAsync(newParticipantId) ?? throw new Exception("User not found");
        conversation.Participants.Add(newParticipant);
        _conversationRepository.Update();
        return conversation;
    }
}
