namespace BookingApp;

public record class EmploymentRequestDto
(
    string SenderId,
    string ReceiverId,
    long FacilityId
);
