using Newtonsoft.Json;

namespace BookingApp;

public record class ServiceCreateDto
(
    string Name,
    decimal Price
);
