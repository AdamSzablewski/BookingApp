namespace BookingApp;

public record class ServiceDto
(
    long Id,
    string Name,
    decimal Price,
    List<EmployeeDto> Employees
);
