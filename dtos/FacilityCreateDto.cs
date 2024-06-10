using System.ComponentModel.DataAnnotations;

namespace BookingApp;

public class FacilityCreateDto
{
    [Required(ErrorMessage = "Name is required.")]
    public required string Name {get; set;}
    
    [Required(ErrorMessage = "Country is required.")]
    public required string Country {get; set;}
    [Required(ErrorMessage = "City is required.")]
    public required string City {get; set;}
    [Required(ErrorMessage = "Street is required.")]
    public required string Street {get; set;}
    [Required(ErrorMessage = "HouseNumber is required.")]
    public required string HouseNumber {get; set;}
    [Required(ErrorMessage = "Category is required.")]
    public required string Category {get; set;}

}
