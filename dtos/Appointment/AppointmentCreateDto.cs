using System.ComponentModel.DataAnnotations;

namespace BookingApp;

public  class AppointmentCreateDto
{
    [Required(ErrorMessage = "ServiceId is required.")]
    public long ServiceId {get; set;}
    [Required(ErrorMessage = "EmployeeId is required.")]
    public long EmployeeId {get; set;}
    [Required(ErrorMessage = "CustomerId is required.")]
    public long CustomerId {get; set;}
    [Required(ErrorMessage = "UserId is required.")]
    public string UserId {get; set;}
    [Required(ErrorMessage = "StartTime is required.")]
    public TimeOnly StartTime {get; set;}
    [Required(ErrorMessage = "EndTime is required.")]
    public TimeOnly EndTime {get; set;}
    [Required(ErrorMessage = "StartTime is required.")]
    public DateOnly Date {get; set;}
    [Required(ErrorMessage = "Backup employees are required.")]
    public List<EmployeeDto> Employees {get; set;}
    
    
};
