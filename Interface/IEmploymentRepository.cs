namespace BookingApp;

public interface IEmploymentRepository
{
    
    public Task<EmploymentRequest> SaveAsync(EmploymentRequest employmentRequest);
    public Task<EmploymentRequest> CreateAsync(EmploymentRequest employmentRequest);

}
