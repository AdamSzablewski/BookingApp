namespace BookingApp;

public record class EmploymentRequestDto
(
    long SenderId,
    long ReceiverId,
    long FacilityId
);
