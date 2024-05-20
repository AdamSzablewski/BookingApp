namespace BookingApp;

public class AppointmentService(IAppointmentRepository appointmentRepository,
IServiceRepository serviceRepository, IEmployeeRepository employeeRepository, ICustomerRepository customerRepository, IReviewRepository reviewRepository)
{
    private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
    private readonly IServiceRepository _serviceRepository = serviceRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IReviewRepository _reviewRepository = reviewRepository;
    private readonly int MINUTE_INCREMENT = 15;

    public async Task<List<TimeSlot>> GetAvailableTimeSlotsForService(long serviceId, DateOnly date){
        Service service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new ServiceNotFoundException();
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
        bool withinWorkingHours = IsWithinWorkingHours(slotStartTime, slotEndTime, employeeStartTime, employeeEndTime);
        foreach(Appointment appointment in appointments){
            bool appointmentOverlaps = AppointmentOverlaps(appointment, slotStartTime, slotEndTime);
            if(!withinWorkingHours || appointmentOverlaps){
                return false;
            }
        }
        return true;
    }
    public bool AppointmentOverlaps(Appointment appointment, DateTime slotStartTime, DateTime slotEndTime){
        if(appointment.Completed)
        {
            return false;
        }
        return (slotStartTime < appointment.EndTime) && (slotEndTime > appointment.StartTime);
    }
    public bool IsWithinWorkingHours(DateTime slotStartTime, DateTime slotEndTime, DateTime employeeStartTime, DateTime employeeEndTime)
    {
        return slotStartTime >= employeeStartTime && slotEndTime <= employeeEndTime;
    }

    public async Task<Appointment> BookAppointment(AppointmentCreateDto appointmentDto)
    {
        Employee employee = await _employeeRepository.GetByIdAsync(appointmentDto.EmployeeId) ?? throw new EmployeeNotFoundException();
        Customer customer = await _customerRepository.GetByIdAsync(appointmentDto.CustomerId) ?? throw new CustomerNotFoundException();
        Service service = await _serviceRepository.GetByIdAsync(appointmentDto.ServiceId) ?? throw new ServiceNotFoundException();
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
        await _appointmentRepository.UpdateAsync();
        return appointment;
        
    }
    public async Task<bool> CancelAppointment(long appointmentId)
    {
        Appointment appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
        if(appointment == null)
        {
            return false;
        }
        Employee employee = appointment.Employee;
        Customer customer = appointment.Customer;
        employee.Appointments.Remove(appointment);
        customer.Appointments.Remove(appointment);
        await _appointmentRepository.DeleteAsync(appointment);
        await _appointmentRepository.UpdateAsync();
        return true;
    }
    public async Task<bool> CloseAppointment(long appointmentId)
    {
        Appointment appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
        if(appointment == null)
        {
            return false;
        }
        appointment.Completed = true;
        Review review = new()
        {
            User = appointment.Customer.User,
            IsActive = true
        };
        await _reviewRepository.CreateAsync(review);
        await _reviewRepository.UpdateAsync();
        return true;
    }
}
