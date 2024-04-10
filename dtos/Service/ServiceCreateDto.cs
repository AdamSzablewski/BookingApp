using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BookingApp;

public record class ServiceCreateDto
(
    string Name,
    decimal Price,
    [Range(00, 24, ErrorMessage = "Hours must be between 0 and 24.")]
    int Hours,
    [Range(0, 59, ErrorMessage = "Minutes must be between 0 and 60.")]
    int Minutes
);
