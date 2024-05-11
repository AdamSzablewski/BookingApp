using System.ComponentModel.DataAnnotations;

namespace BookingApp;

public class ReviewCreateDto
{
    [Required(ErrorMessage = "Id field is required.")]
    public long Id {get; set;}
    [Required(ErrorMessage = "FacilityId is required.")]
    public long FacilityId {get; set;}
    [Required(ErrorMessage = "UserId is required.")]
    public required string UserId {get; set;}
    [Required(ErrorMessage = "Points are required.")]
    [Range(1, 10, ErrorMessage = "Points must be between 1 - 10")]
    public int Points {get; set;}
    public string Text {get; set;}
}
