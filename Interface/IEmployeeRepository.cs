namespace BookingApp;

public interface IEmployeeRepository
{
    public Task<Employee> GetById(long Id);
    public Task<Employee> Create(Employee employee);
    public Task<Employee> Delete(Employee employee);
    public Task<Employee> Update(Employee employee);

}
