using System.ComponentModel.DataAnnotations;

namespace BookingApp;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string? Username {get; set;}
    [Required]
    public string? Password {get; set;}

}
