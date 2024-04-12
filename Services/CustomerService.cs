namespace BookingApp;

public class CustomerService
{
    private readonly CustomerRepository _customerRepository;
    public CustomerService(CustomerRepository customerRepository){
        _customerRepository = customerRepository;
    }

    
}
