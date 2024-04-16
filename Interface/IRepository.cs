namespace BookingApp;

public interface IRepository<T, R>
{
    T? GetById(R Id);
    T Create(T obj);
    bool Delete(T obj);
    bool Update();
    Task<T?> GetByIdAsync(R Id);
    Task<T> CreateAsync(T obj);
    bool UpdateAsync();

}
