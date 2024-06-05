namespace BookingApp;

public static class ServiceMapper
{
    public static ServiceDto MapToDto(this Service service){
       
        return new ServiceDto(){
            Id = service.Id,
            Name = service.Name,
            Price = service.Price,
            Duration = service.Length,
            Employees = service.Employees == null ? null : service.Employees.Select(e => e.MapToDto()).ToList()
        };
          
    
    }

}
