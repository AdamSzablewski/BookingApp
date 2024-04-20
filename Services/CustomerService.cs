namespace BookingApp;

public class CustomerService(CustomerRepository customerRepository, PersonRepository personRepository)
{
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly PersonRepository _personRepository = personRepository;

    public async Task<Customer> CreateCustomer(string userId)
    {
        Person user = await _personRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException();
        Customer customer = new()
        {
            User = user,
        };
        await _customerRepository.CreateAsync(customer);
        return customer;
    }

}
