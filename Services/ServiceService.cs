using System.Collections;

namespace BookingApp;

public class ServiceService(IServiceRepository serviceRepository, IFacilityRepository facilityRepository, IEmployeeRepository employeeRepository)
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;
    private readonly IFacilityRepository _facilityRepository = facilityRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<ServiceDto> GetByIdAsync(long serviceId){
        Service service =  await _serviceRepository.GetByIdAsync(serviceId) ?? throw new Exception("Service Not found");
      
        return service.MapToDto();
        
    }
    public async Task<List<ServiceDto>> GetAllForFacilityAsync(long facilityId){
        List<Service> services = await _serviceRepository.GetAllForFacility(facilityId);
        List<ServiceDto> serviceDtos = services.Select(e => e.MapToDto()).ToList();
        return serviceDtos;
    }
    public async Task<Service> CreateAsync(long FacilityId, ServiceCreateDto serviceCreateDto){
        Facility facility = await _facilityRepository.GetByIdAsync(FacilityId) ?? throw new FacilityNotFoundException() ;
        TimeSpan serviceLength = new(serviceCreateDto.Hours, serviceCreateDto.Minutes, 0);
        Service service = new()
        {
            Name = serviceCreateDto.Name,
            Price = serviceCreateDto.Price,
            Facility = facility,
            Length = serviceLength
        };
        await _serviceRepository.CreateAsync(service);
        await _serviceRepository.UpdateAsync();
        return service;
    }
    public async Task<Service> UpdateAsync(long ServiceId, ServiceCreateDto serviceDto){
        Service service = await _serviceRepository.GetByIdAsync(ServiceId) ?? throw new ServiceNotFoundException();
        service.Name = serviceDto.Name;
        service.Price = serviceDto.Price;
        await _serviceRepository.UpdateAsync();
        return service;
    }
    public async Task<Service> ChangePriceAsync(long ServiceId, decimal NewPrice){
        Service service = await _serviceRepository.GetByIdAsync(ServiceId) ?? throw new ServiceNotFoundException();
        service.Price = NewPrice;
        await _serviceRepository.UpdateAsync();
        return service;
    }
    public async Task<Service> ChangeNameAsync(long ServiceId, string NewName){
        Service service = await _serviceRepository.GetByIdAsync(ServiceId) ?? throw new ServiceNotFoundException();
        service.Name = NewName;
        await _serviceRepository.UpdateAsync();
        return service;
    }
        
    public async Task<Service> AddEmployeeToService(long employeeId, long serviceId){
        Service service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new ServiceNotFoundException();
        Employee employee = await _employeeRepository.GetByIdAsync(employeeId) ?? throw new EmployeeNotFoundException(); 
        service.Employees.Add(employee);
        await _serviceRepository.UpdateAsync();
        return service;
    }

    public async Task<object?> GetEmployeesForTask(long serviceId)
    {
        Service? service = await _serviceRepository.GetByIdAsync(serviceId);
        if(service == null)
        {
            return null;
        }
        List<EmployeeDto> employeeDtos =  service.Employees.MapToDto();
        EmployeeDto presetAny = new()
        {
            Id = 0,
            FirstName = "Any",
            LastName =""

        };
        employeeDtos.Insert(0, presetAny);
        return employeeDtos;
    }
}
