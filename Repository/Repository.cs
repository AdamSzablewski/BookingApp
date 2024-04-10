
namespace BookingApp;

public abstract class Repository<T> : IRepository<T>
{
    protected readonly BookingAppContext _dbContext;
    public Repository(BookingAppContext dbContext)
    {
        _dbContext = dbContext;
    }

    public T Create(T obj)
    {
        _dbContext.Add(obj);
        Update();
        return obj;
    }
    public async Task<T> CreateAsync(T obj)
    {
        await _dbContext.AddAsync(obj);
        UpdateAsync();
        return obj;
    }
    public bool Delete(long obj)
    {
        _dbContext.Remove(obj);
        Update();
        return true;
    }
    public bool Update()
    {
        _dbContext.SaveChanges();
        return true;
    }

    public bool UpdateAsync()
    {
        _dbContext.SaveChangesAsync();
        return true;
    }

    public abstract T? GetById(long Id);

    public abstract Task<T?> GetByIdAsync(long Id);
}
