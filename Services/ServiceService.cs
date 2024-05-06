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
        Facility facility = await _facilityRepository.GetByIdAsync(FacilityId);
        TimeSpan serviceLength = new TimeSpan(serviceCreateDto.Hours, serviceCreateDto.Minutes, 0);
        if(facility == null){
            throw new Exception("Facility Not found");
        }
        Service service = new(){
            Name = serviceCreateDto.Name,
            Price = serviceCreateDto.Price,
            Facility = facility,
            Length = serviceLength

        };
        return await _serviceRepository.CreateAsync(service);
    }
    public async Task<Service> UpdateAsync(long ServiceId, ServiceCreateDto serviceDto){
        Service service = await _serviceRepository.GetByIdAsync(ServiceId);
        if(service == null){
            throw new Exception("Service Not Found");
        }
        service.Name = serviceDto.Name;
        service.Price = serviceDto.Price;
        _serviceRepository.UpdateAsync();
        return service;
    }
    public async Task<Service> ChangePriceAsync(long ServiceId, decimal NewPrice){
        Service service = await _serviceRepository.GetByIdAsync(ServiceId);
        if(service == null){
            throw new Exception("Service Not Found");
        }
        service.Price = NewPrice;
        _serviceRepository.UpdateAsync();
        return service;
    }
    public async Task<Service> ChangeNameAsync(long ServiceId, string NewName){
        Service service = await _serviceRepository.GetByIdAsync(ServiceId) ?? throw new Exception("Service Not Found");
        service.Name = NewName;
        _serviceRepository.UpdateAsync();
        return service;
    }
        
    public async Task<Service> AddEmployeeToService(long employeeId, long serviceId){
        Service service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new Exception("Service Not Found");
        Employee employee = await _employeeRepository.GetByIdAsync(employeeId) ?? throw new Exception("Employee Not Found"); 
        service.Employees.Add(employee);
        _serviceRepository.UpdateAsync();
        return service;
    }

}
