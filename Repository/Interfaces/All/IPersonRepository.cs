namespace BookingApp;

public interface IPersonRepository : IRepository<Person, string>
{
     public Task<List<Person>> GetPeopleAsync(List<string> Ids);
     public Task<List<Person>> GetPeople(List<string> Ids);
     public Task<Person?> GetByEmailAsync(string email);
     
}
