namespace BookingApp;

public class EmploymentService(
    IPersonRepository personRepository,
    IEmployeeRepository employeeRepository,
    IEmploymentRequestRepository employmentRequestRepository)
{
    private readonly IPersonRepository _personRepository = personRepository;
    private readonly IEmploymentRequestRepository _employmentRequestRepository = employmentRequestRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<EmploymentRequest> SendEmploymentRequest(EmploymentRequestDto employmentRequestDto){
        Person receiverPerson = await _personRepository.GetByIdAsync(employmentRequestDto.ReceiverId) ?? throw new UserNotFoundException();
        Person senderPerson = await _personRepository.GetByIdAsync(employmentRequestDto.SenderId) ?? throw new UserNotFoundException();

        Employee employee = receiverPerson.Employee ?? throw new EmployeeNotFoundException("User is not an Employee");
        Owner owner = senderPerson.Owner ?? throw new OwnerNotFoundException("User is not an Owner");
        Facility facility = owner.Facilities.FirstOrDefault(f => f.Id == employmentRequestDto.FacilityId) ?? throw new FacilityNotFoundException();
        EmploymentRequest employmentRequest = new()
        {
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
        await _employmentRequestRepository.UpdateAsync();
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
        await _employmentRequestRepository.UpdateAsync();
    
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
