namespace BookingApp;

public class AppointmentService
{
    private readonly AppointmentRepository _appointmentRepository;
    private readonly ServiceRepository _serviceRepository;
    private readonly EmployeeRepository _employeeRepository;
    private readonly CustomerRepository _customerRepository;
    private readonly int MINUTE_INCREMENT = 15;
    public AppointmentService(AppointmentRepository appointmentRepository, 
    ServiceRepository serviceRepository, EmployeeRepository employeeRepository, CustomerRepository customerRepository)
    {
        _appointmentRepository = appointmentRepository;
        _serviceRepository = serviceRepository;
        _employeeRepository = employeeRepository;
        _customerRepository = customerRepository;
    }
    public async Task<List<TimeSlot>> GetAvailableTimeSlotsForService(long serviceId, DateOnly date){
        Service service = await _serviceRepository.GetByIdAsync(serviceId);
        List<Employee> employeesForTask = service.Employees;
        if(employeesForTask == null){
            return new List<TimeSlot>();
        }
        TimeSpan serviceDuration = service.Length;
       
        List<TimeSlot> timeSlots = [];
        foreach(Employee employee in employeesForTask){
        
            DateTime currentTime = new DateTime(date.Year, date.Month, date.Day, service.Facility.StartTime.Hour, service.Facility.StartTime.Minute,  service.Facility.StartTime.Second);
            DateTime bufferedTime = currentTime.AddHours(serviceDuration.Hours).AddMinutes(serviceDuration.Minutes);
            DateTime employeeStartTime = new DateTime(date.Year, date.Month, date.Day, employee.StartTime.Hour, employee.StartTime.Minute,  employee.StartTime.Second);
            DateTime employeeEndTime = new DateTime(date.Year, date.Month, date.Day, employee.EndTime.Hour, employee.EndTime.Minute,  employee.EndTime.Second);

            while(employeeStartTime <= currentTime && bufferedTime <= employeeEndTime){
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
               
                currentTime = currentTime.AddMinutes(MINUTE_INCREMENT);
                bufferedTime = bufferedTime.AddMinutes(MINUTE_INCREMENT);
               
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

    public async Task<Appointment> BookAppointment(AppointmentCreateDto appointmentDto)
    {
        Employee employee = await _employeeRepository.GetByIdAsync(appointmentDto.EmployeeId) ?? throw new Exception("Employee not found");
        Customer customer = await _customerRepository.GetByIdAsync(appointmentDto.EmployeeId) ?? throw new Exception("Customer not found");
        Service service = await _serviceRepository.GetByIdAsync(appointmentDto.ServiceId) ?? throw new Exception("Service not found");
        Appointment appointment = new(){
            ServiceId = appointmentDto.ServiceId,
            Employee = employee,
            Customer = customer,
            StartTime = appointmentDto.StartTime,
            EndTime = appointmentDto.EndTime,
            Service = service
        };
        await _appointmentRepository.CreateAsync(appointment);
        employee.Appointments.Add(appointment);
        customer.Appointments.Add(appointment);
        _appointmentRepository.UpdateAsync();
        return appointment;
        

    }
}
