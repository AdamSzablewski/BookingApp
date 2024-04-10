namespace BookingApp;

public interface IRepository<T>
{
    T? GetById(long Id);
    T Create(T obj);
    bool Delete(long obj);
    bool Update();
    Task<T?> GetByIdAsync(long Id);
    Task<T> CreateAsync(T obj);
    bool UpdateAsync();

}
