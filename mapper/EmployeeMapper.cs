namespace BookingApp;

public static class EmployeeMapper
{
    public static EmployeeDto MapToDto(this Employee employee){
        if(employee == null) return null;
        return new EmployeeDto(employee.Id, employee.User.MapToDto());
    }

}
