namespace BookingApp;

public record class FacilityCreateDto
(
    string Name,
    long OwnerId,
    string Country,
    string City,
    string Street,
    string HouseNumber

);
