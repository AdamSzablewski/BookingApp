using System.ComponentModel.DataAnnotations;

namespace BookingApp;

public class ConversationPerson
{
    public long Id {get; set;}
    public long ConversationId {get; set;}
    public string PersonId {get; set;}

    [Required]
    public Conversation Conversation {get; set;}
    
    [Required]
    public Person Person {get; set;}

}
