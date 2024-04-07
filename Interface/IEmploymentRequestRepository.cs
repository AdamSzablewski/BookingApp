namespace BookingApp;

public interface IEmploymentRequestRepository
{
    public Task<EmploymentRequest?> GetById(long Id);
    public Task<EmploymentRequest> Create(EmploymentRequest employmentRequest);
    public Task<EmploymentRequest?> Update(EmploymentRequest employmentRequest);
    public Task<EmploymentRequest?> Delete(long Id);

}
