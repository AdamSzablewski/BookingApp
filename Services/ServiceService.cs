using System.Collections;

namespace BookingApp;

public class ServiceService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IFacilityRepository _facilityRepository;
    public ServiceService(IServiceRepository serviceRepository, IFacilityRepository facilityRepository){
        _serviceRepository = serviceRepository;
        _facilityRepository = facilityRepository;
    }
     public async Task<ServiceDto> GetByIdAsync(long serviceId){
        Service service =  await _serviceRepository.GetByIdAsync(serviceId);
        if(service == null){
            throw new Exception("Service Not found");
        }
        return service.MapToDto();
        
    }
    public async Task<List<ServiceDto>> GetAllForFacilityAsync(long facilityId){
        List<Service> services = await _serviceRepository.GetAllForFacility(facilityId);
        List<ServiceDto> serviceDtos = services.Select(e => e.MapToDto()).ToList();
        return serviceDtos;
    }
    public async Task<Service> CreateAsync(long FacilityId, ServiceCreateDto serviceCreateDto){
        Facility facility = await _facilityRepository.GetByIdAsync(FacilityId);
    
        if(facility == null){
            throw new Exception("Facility Not found");
        }
        Service service = new(){
            Name = serviceCreateDto.Name,
            Price = serviceCreateDto.Price,
            Facility = facility
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
        return await _serviceRepository.UpdateAsync(service);
    }
    public async Task<Service> ChangePriceAsync(long ServiceId, decimal NewPrice){
        Service service = await _serviceRepository.GetByIdAsync(ServiceId);
        if(service == null){
            throw new Exception("Service Not Found");
        }
        service.Price = NewPrice;
        return await _serviceRepository.UpdateAsync(service);
    }
    public async Task<Service> ChangeNameAsync(long ServiceId, string NewName){
        Service service = await _serviceRepository.GetByIdAsync(ServiceId);
        if(service == null){
            throw new Exception("Service Not Found");
        }
        service.Name = NewName;
        return await _serviceRepository.UpdateAsync(service);
    }

}
