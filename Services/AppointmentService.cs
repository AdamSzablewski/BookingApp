namespace BookingApp;

public class AppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly int MINUTE_INCREMENT = 15;
    public AppointmentService(IAppointmentRepository appointmentRepository, IServiceRepository serviceRepository){
        _appointmentRepository = appointmentRepository;
        _serviceRepository = serviceRepository;
    }
    public async Task<List<TimeSlot>> GetAvailableTimeSlotsForService(long serviceId, DateOnly date){
        Service service = await _serviceRepository.GetByIdAsync(serviceId);
        List<Employee> employeesForTask = service.Employees;
        if(employeesForTask == null){
            return new List<TimeSlot>();
        }
        TimeSpan serviceDuration = service.Length;
       
        List<TimeSlot> timeSlots = new List<TimeSlot>();
        foreach(Employee employee in employeesForTask){
        
            DateTime currentTime = new DateTime(date.Year, date.Month, date.Day, service.Facility.StartTime.Hour, service.Facility.StartTime.Minute,  service.Facility.StartTime.Second);
            DateTime bufferedTime = currentTime.AddHours(serviceDuration.Hours).AddMinutes(serviceDuration.Minutes);
            DateTime employeeStartTime = new DateTime(date.Year, date.Month, date.Day, employee.StartTime.Hour, employee.StartTime.Minute,  employee.StartTime.Second);
            DateTime employeeEndTime = new DateTime(date.Year, date.Month, date.Day, employee.StartTime.Hour, employee.StartTime.Minute,  employee.StartTime.Second);

            while(employeeStartTime >= currentTime && bufferedTime <= employeeEndTime){
                bool available = await CheckIfTimeSlotAvailable(currentTime, bufferedTime, employee, date);
                if(available){
                    TimeSlot timeSlot = new TimeSlot
                    (
                        employee.MapToDto(),
                        currentTime,
                        bufferedTime
                    );
                timeSlots.Add(timeSlot);
                }
               
                currentTime.AddMinutes(MINUTE_INCREMENT);
                bufferedTime.AddMinutes(MINUTE_INCREMENT);
                Console.WriteLine("Writing line    ");
            }
        }
        return timeSlots;
    }

    public async Task<bool> CheckIfTimeSlotAvailable(DateTime slotStartTime, DateTime slotEndTime, Employee employee, DateOnly date){
        List<Appointment> appointments = employee.Appointments;
        DateTime employeeStartTime = new DateTime(date.Year, date.Month, date.Day, employee.StartTime.Hour, employee.StartTime.Minute,  employee.StartTime.Second);
        DateTime employeeEndTime = new DateTime(date.Year, date.Month, date.Day, employee.StartTime.Hour, employee.StartTime.Minute,  employee.StartTime.Second);

        foreach(Appointment appointment in appointments){
            if(slotStartTime < employeeStartTime || slotEndTime > employeeEndTime){
                return false;
            }
        }
        return true;
    }
}
