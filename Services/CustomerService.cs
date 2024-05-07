namespace BookingApp;

public class CustomerService(ICustomerRepository customerRepository, IPersonRepository personRepository)
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IPersonRepository _personRepository = personRepository;

    public async Task<Customer> CreateCustomer(string userId)
    {
        Person user = await _personRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException();
        Customer customer = new()
        {
            User = user,
        };
        await _customerRepository.CreateAsync(customer);
        await _customerRepository.UpdateAsync();
        return customer;
    }

}
