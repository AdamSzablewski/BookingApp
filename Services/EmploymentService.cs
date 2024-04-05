namespace BookingApp;

public class EmploymentService
{
    private readonly IPersonRepository _personRepository;
    private readonly IEmploymentRepository _employmentRepository;

    public EmploymentService(IPersonRepository personRepository, IEmploymentRepository employmentRepository){
        _personRepository = personRepository;
        _employmentRepository = employmentRepository;
    }

    public async Task<EmploymentRequest> SendEmploymentRequest(EmploymentRequestDto employmentRequestDto){
        Person receiverPerson = await _personRepository.GetByIdAsync(employmentRequestDto.ReceiverId);
        Person senderPerson = await _personRepository.GetByIdAsync(employmentRequestDto.SenderId);

        Employee employee = receiverPerson.Employee;
         Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!");
        Console.WriteLine(receiverPerson.ToString());
        Console.WriteLine(employmentRequestDto);
        if(employee == null)
        {
            throw new Exception("User is not an Employee");
        }
        Owner owner = senderPerson.Owner;
        if(owner == null)
        {
            throw new Exception("User is not an Owner");
        }
        EmploymentRequest employmentRequest = new EmploymentRequest{
            Sender = owner,
            Receiver = employee
        };
        employee.EmploymentRequests.Add(employmentRequest);
        owner.ActiveRequests.Add(employmentRequest);
        await _employmentRepository.SaveAsync(employmentRequest);
        return employmentRequest;
    }

}
