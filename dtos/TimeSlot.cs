namespace BookingApp;

public record class TimeSlot
(
    EmployeeDto EmployeeDto,
    DateTime StartTime,
    DateTime EndTime
);
