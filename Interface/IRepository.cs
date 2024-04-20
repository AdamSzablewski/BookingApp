namespace BookingApp;

public interface IRepository<T, R>
{
    T? GetById(R Id);
    T Create(T obj);
    bool Delete(T obj);
    bool Update();
    Task<ICollection<T>> CreateAllAsync(ICollection<T> values);
    ICollection<T> CreateAll(ICollection<T> values);
    Task<T?> GetByIdAsync(R Id);
    Task<T> CreateAsync(T obj);
    Task<bool> UpdateAsync();

}
