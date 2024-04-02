namespace BookingApp;

public record class PersonCreateDto
(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password
);
