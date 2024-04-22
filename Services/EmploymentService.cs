namespace BookingApp;

public class EmploymentService(
    PersonRepository personRepository,
    EmployeeRepository employeeRepository,
    EmploymentRequestRepository employmentRequestRepository,
    BookingAppContext dbContext)
{
    private readonly PersonRepository _personRepository = personRepository;
    private readonly EmploymentRequestRepository _employmentRequestRepository = employmentRequestRepository;
    private readonly EmployeeRepository _employeeRepository = employeeRepository;
    private readonly BookingAppContext _dbContext = dbContext;

    public async Task<EmploymentRequest> SendEmploymentRequest(EmploymentRequestDto employmentRequestDto){
        Person? receiverPerson = await _personRepository.GetByIdAsync(employmentRequestDto.ReceiverId) ?? throw new Exception("User not found");
        Person? senderPerson = await _personRepository.GetByIdAsync(employmentRequestDto.SenderId) ?? throw new Exception("User not found");

        Employee employee = receiverPerson.Employee ?? throw new Exception("User is not an Employee");
        Owner owner = senderPerson.Owner ?? throw new Exception("User is not an Owner");
        Facility facility = owner.Facilities.FirstOrDefault(f => f.Id == employmentRequestDto.FacilityId) ?? throw new Exception("Facility not found");
        EmploymentRequest employmentRequest = new EmploymentRequest{
            Sender = owner,
            Receiver = employee,
            Facility = facility
        };
        await _employmentRequestRepository.CreateAsync(employmentRequest);
        employee.EmploymentRequests.Add(employmentRequest);
        owner.ActiveRequests.Add(employmentRequest);
        await _employmentRequestRepository.UpdateAsync();
        return employmentRequest;
    }

    public async Task<EmploymentRequest> AnswereEmploymentRequest(long requestId, string userId, bool decision){
        EmploymentRequest? employmentRequest = await _employmentRequestRepository.GetByIdAsync(requestId) ?? throw new Exception("No such employment request exist");
        Person receiverUser = employmentRequest.Receiver.User;
        if(!receiverUser.Id.Equals(userId))
        {
            throw new NotAuthorizedException();
        }
        if(decision){
            AcceptRequest(employmentRequest);
        }else{
            DeclineRequest(employmentRequest);
        }
        return employmentRequest;
    }

    private async void DeclineRequest(EmploymentRequest employmentRequest)
    {
        employmentRequest.Closed = true;
        employmentRequest.Decision = false;
        _employmentRequestRepository.UpdateAsync();
    }

    public async void AcceptRequest(EmploymentRequest employmentRequest){
        Facility facility = employmentRequest.Facility;
        Employee employee = employmentRequest.Receiver;
        employee.StartTime = facility.StartTime;
        employee.EndTime = facility.EndTime;
        facility.Employees.Add(employee);
        employee.Workplace = facility;

        employmentRequest.Closed = true;
        employmentRequest.Decision = true;
        _employmentRequestRepository.UpdateAsync();
    
    }
    public async Task<Employee> CreateEmployee(string userId)
    {
        Person user = await _personRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException();
        Employee employee = new (){
            User = user,
            StartTime = new TimeOnly(9,0),
            EndTime = new TimeOnly(17,0),
        };
        
        await _employeeRepository.CreateAsync(employee);

        user.Employee = employee;
        await _personRepository.UpdateAsync();
        return employee;
    }

}
