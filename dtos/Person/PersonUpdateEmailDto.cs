using System.ComponentModel.DataAnnotations;

namespace BookingApp;

public class PersonUpdateEmailDto
{
    [Required(ErrorMessage = "Email is required.")]
    public string Email {get; set;}
}
