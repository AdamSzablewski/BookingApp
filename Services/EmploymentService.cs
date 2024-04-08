namespace BookingApp;

public class EmploymentService
{
    private readonly IPersonRepository _personRepository;
    private readonly IEmploymentRequestRepository _employmentRequestRepository;
    private readonly IEmploymentRepository _employmentRepository;
    private readonly BookingAppContext _dbContext;

    public EmploymentService(
        IPersonRepository personRepository,
        IEmploymentRepository employmentRepository,
        IEmploymentRequestRepository employmentRequestRepository,
        BookingAppContext dbContext)
    {
        _personRepository = personRepository;
        _employmentRepository = employmentRepository;
        _employmentRequestRepository = employmentRequestRepository;
        _dbContext = dbContext;
    }

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
        employee.EmploymentRequests.Add(employmentRequest);
        owner.ActiveRequests.Add(employmentRequest);
        await _employmentRepository.SaveAsync(employmentRequest);
        return employmentRequest;
    }

    public async Task<EmploymentRequest> AnswereEmploymentRequest(long requestId, bool decision){
        EmploymentRequest? employmentRequest = await _employmentRequestRepository.GetById(requestId) ?? throw new Exception("No such employment request exist");
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
        await _employmentRequestRepository.Update(employmentRequest);
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
        await _employmentRequestRepository.Update(employmentRequest);
    
    }

}
