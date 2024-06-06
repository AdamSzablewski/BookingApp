namespace BookingApp;

public static class EmployeeMapper
{
    public static EmployeeDto? MapToDto(this Employee employee){
        if(employee == null) return null;
        return new EmployeeDto()
        {
            Id = employee.Id,
            FirstName = employee.User?.FirstName,
            LastName = employee.User?.LastName,
            ProfilePicture = employee.User?.ProfilePicture
        };
    }
    public static List<EmployeeDto> MapToDto(this List<Employee> employees){
        if(employees == null) return null;
        List<EmployeeDto> result = [];
        foreach (Employee e in employees)
        {
            result.Add(e.MapToDto());
        }
        return result;
    }


}
