namespace BookingApp;

public static class ServiceMapper
{
    public static ServiceDto MapToDto(this Service service){
       
        return new ServiceDto(
            service.Id,
            service.Name,
            service.Price,
            service.Employees == null ? null : service.Employees.Select(e => e.MapToDto()).ToList());
    }

}
