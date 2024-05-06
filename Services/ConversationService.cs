﻿namespace BookingApp;

public class ConversationService(IConversationRepository conversationRepository, IPersonRepository personRepository, ConversationPersonRepository conversationPersonRepository, BookingAppContext dbContext)
{
    private readonly IConversationRepository _conversationRepository = conversationRepository;
    private readonly IPersonRepository _personRepository = personRepository;
    private readonly ConversationPersonRepository _conversationPersonRepository = conversationPersonRepository;
    private readonly BookingAppContext _dbContext = dbContext;
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
        List<Person> users = await _personRepository.GetPeopleAsync(conversationCreateDto.Participants);

        Conversation conversation = new();
        await _conversationRepository.CreateAsync(conversation);
        foreach(Person user in users)
        {
            ConversationPerson conversationPerson = new()
            {
                Person = user,
                Conversation = conversation
            };
            await _conversationPersonRepository.CreateAsync(conversationPerson);
        }        
        return conversation;
    }

    public async Task<Conversation> LeaveConversation(string personId, long conversationId)
    {
        Person user = await _personRepository.GetByIdAsync(personId) ?? throw new Exception("User not found");
        Conversation conversation = await _conversationRepository.GetByIdAsync(conversationId) ?? throw new Exception("Conversation not found");
        if(conversation.Participants.Count > 0)
        {
            DeleteConversation(conversation);
        }
        else
        {
            RemoveUserFromConversation(user, conversation);
        }
        
        return conversation;
    }
    public void RemoveUserFromConversation(Person person, Conversation conversation)
    {
        ConversationPerson conversationPerson = conversation.Participants.FirstOrDefault(e => e.PersonId.Equals(person.Id)) ?? throw new UserNotFoundException();
        _conversationPersonRepository.Delete(conversationPerson);
    
    }
    public void RemoveUserFromConversation(ConversationPerson person)
    {
        _conversationPersonRepository.Delete(person);
    }
    public void DeleteConversation(Conversation conversation)
    {
        if(conversation.Participants.Count > 0)
        {
            _conversationRepository.Delete(conversation);
        }
        else
        {
            foreach(ConversationPerson participant in conversation.Participants)
            {
                RemoveUserFromConversation(participant);
            }
        }
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
        ConversationPerson conversationPerson = new()
            {
                Person = newParticipant,
                Conversation = conversation
            };
        await _conversationPersonRepository.CreateAsync(conversationPerson);
        _conversationPersonRepository.UpdateAsync();
        return conversation;
    }
}
