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
        Service service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new Exception("Service not found");
        List<Employee> employeesForTask = service.Employees;
        TimeSpan serviceDuration = service.Length;
       
        List<TimeSlot> timeSlots = [];
        foreach(Employee employee in employeesForTask){
        
            DateTime currentTime = new(date.Year, date.Month, date.Day, service.Facility.StartTime.Hour, service.Facility.StartTime.Minute,  service.Facility.StartTime.Second);
            DateTime bufferedTime = currentTime.AddHours(serviceDuration.Hours).AddMinutes(serviceDuration.Minutes);
            DateTime employeeStartTime = new(date.Year, date.Month, date.Day, employee.StartTime.Hour, employee.StartTime.Minute,  employee.StartTime.Second);
            DateTime employeeEndTime = new(date.Year, date.Month, date.Day, employee.EndTime.Hour, employee.EndTime.Minute,  employee.EndTime.Second);

            while(employeeStartTime <= currentTime && bufferedTime <= employeeEndTime){
                bool available = CheckIfTimeSlotAvailable(currentTime, bufferedTime, employee, date);
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

    public bool CheckIfTimeSlotAvailable(DateTime slotStartTime, DateTime slotEndTime, Employee employee, DateOnly date){
        List<Appointment> appointments = employee.Appointments;
        DateTime employeeStartTime = new(date.Year, date.Month, date.Day, employee.StartTime.Hour, employee.StartTime.Minute,  employee.StartTime.Second);
        DateTime employeeEndTime = new(date.Year, date.Month, date.Day, employee.EndTime.Hour, employee.EndTime.Minute,  employee.EndTime.Second);
        bool withinWorkingHours = WithinWorkingHours(slotStartTime, slotEndTime, employeeStartTime, employeeEndTime);
        foreach(Appointment appointment in appointments){
            bool appointmentOverlaps = AppointmentOverlaps(appointment, slotStartTime, slotEndTime);
            if(!withinWorkingHours || appointmentOverlaps){
                return false;
            }
        }
        return true;
    }
    public bool AppointmentOverlaps(Appointment appointment, DateTime slotStartTime, DateTime slotEndTime){
        return (slotStartTime < appointment.EndTime) && (slotEndTime > appointment.StartTime);
    }
    public bool WithinWorkingHours(DateTime slotStartTime, DateTime slotEndTime, DateTime employeeStartTime, DateTime employeeEndTime)
    {
        return slotStartTime >= employeeStartTime && slotEndTime <= employeeEndTime;
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
    public async Task<Appointment> CancelAppointment(long appointmentId)
    {
        Appointment appointment = await _appointmentRepository.GetByIdAsync(appointmentId) ?? throw new Exception("No such appointment found");
        
        Employee employee = appointment.Employee;
        Customer customer = appointment.Customer;
        employee.Appointments.Remove(appointment);
        customer.Appointments.Remove(appointment);
        _appointmentRepository.Delete(appointment);
        return appointment;
        //todo send mail with info
    }
}
