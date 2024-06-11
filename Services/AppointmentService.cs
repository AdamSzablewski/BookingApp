namespace BookingApp;

public class AppointmentService(IAppointmentRepository appointmentRepository,
IServiceRepository serviceRepository, IEmployeeRepository employeeRepository, ICustomerRepository customerRepository, IReviewRepository reviewRepository, IPersonRepository personRepository)
{
    private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
    private readonly IServiceRepository _serviceRepository = serviceRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IReviewRepository _reviewRepository = reviewRepository;
    private readonly IPersonRepository _personRepository = personRepository;
    private readonly int MINUTE_INCREMENT = 15;

        public async Task<List<TimeSlot>> GetAvailableTimeSlotsForService(long serviceId, DateOnly date){
        Service service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new ServiceNotFoundException();
        Facility facility = service.Facility;
        List<Employee> employeesForTask = service.Employees;
        TimeSpan serviceDuration = service.Length;
         EmployeeDto presetAny = new()
        {
            Id = 0,
            FirstName = "Any",
            LastName =""

        };
        DateTime currentTime = new(date.Year, date.Month, date.Day, service.Facility.StartTime.Hour, service.Facility.StartTime.Minute,  service.Facility.StartTime.Second);
        DateTime bufferedTime = currentTime.AddHours(serviceDuration.Hours).AddMinutes(serviceDuration.Minutes);
        DateTime maxTime = new(date.Year, date.Month, date.Day, facility.EndTime.Hour, facility.EndTime.Minute, facility.EndTime.Second);  
        List<TimeSlot> timeSlots = [];
        while(bufferedTime < maxTime)
        {
            TimeSlot timeSlot = new TimeSlot
                    {
                        Employees = [],
                        StartTime = new TimeOnly(currentTime.Hour, currentTime.Minute, currentTime.Second),
                        EndTime = new TimeOnly(bufferedTime.Hour, bufferedTime.Minute, bufferedTime.Second),
                        Date = new DateOnly(currentTime.Year, currentTime.Month, currentTime.Day)
                    };
           foreach(Employee employee in employeesForTask)
           {
                bool anyAvailable = false;
                DateTime employeeStartTime = new(date.Year, date.Month, date.Day, employee.StartTime.Hour, employee.StartTime.Minute,  employee.StartTime.Second);
                DateTime employeeEndTime = new(date.Year, date.Month, date.Day, employee.EndTime.Hour, employee.EndTime.Minute,  employee.EndTime.Second);
                bool available = CheckIfTimeSlotAvailable(currentTime, bufferedTime, employee, date);
                if(available)
                {
                    timeSlot.Employees.Add(employee.MapToDto());
                    timeSlot.Employees.Add(presetAny);
                    
                }
           } 
            timeSlots.Add(timeSlot);
            currentTime = currentTime.AddMinutes(MINUTE_INCREMENT);
            bufferedTime = bufferedTime.AddMinutes(MINUTE_INCREMENT); 
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
    public long GetBackupForTask(List<long> ids)
    {
        int i = 0;
        while(i < ids.Count && ids[i] != 0)
        {
            if(ids[i] != 0)
            {
                return ids[i];
            }
            i++;
        }
        return -1;
    }

    public async Task<Appointment?> BookAppointment(AppointmentCreateDto appointmentDto, string userId)
    {
        long employeeId;
        if(appointmentDto.EmployeeId == 0)
        {
            employeeId = GetBackupForTask(appointmentDto.Employees.Select(employee => employee.Id).ToList());
        }
        else
        {
            employeeId = appointmentDto.EmployeeId;
        }
        Console.WriteLine(appointmentDto);

        if(employeeId == -1)
        {
            return null;
        }
        Person user = await _personRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException();
        Customer? customer = user.Customer ?? throw new CustomerNotFoundException();
        Employee employee = await _employeeRepository.GetByIdAsync(employeeId) ?? throw new EmployeeNotFoundException();
        Service service = await _serviceRepository.GetByIdAsync(appointmentDto.ServiceId) ?? throw new ServiceNotFoundException();
        Appointment appointment = new(){
            ServiceId = appointmentDto.ServiceId,
            Employee = employee,
            Customer = customer,
            StartTime = new DateTime(appointmentDto.Date, appointmentDto.StartTime),
            EndTime = new DateTime(appointmentDto.Date, appointmentDto.EndTime),
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

    internal async Task<List<AppointmentDto>> GetLatestAppointment(string userId)
    {
        List<Appointment> appointments = await _appointmentRepository.GetForUser(userId);
        return appointments.MapToDto();
    }
}
