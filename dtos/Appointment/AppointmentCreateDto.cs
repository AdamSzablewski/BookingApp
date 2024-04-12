using System.ComponentModel.DataAnnotations;

namespace BookingApp;

public record class AppointmentCreateDto
(
    [Required(ErrorMessage = "ServiceId is required.")]
    long ServiceId,
    [Required(ErrorMessage = "EmployeeId is required.")]
    long EmployeeId,
    [Required(ErrorMessage = "CustomerId is required.")]
    long CustomerId,
    [Required(ErrorMessage = "StartTime is required.")]
    DateTime StartTime,
    [Required(ErrorMessage = "EndTime is required.")]
    DateTime EndTime
);
