
namespace BookingApp;

public abstract class Repository<T, R> : IRepository<T, R>
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
    public bool Delete(T obj)
    {
        var removed = _dbContext.Remove(obj);
        Update();
        return removed != null;
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

    public abstract T? GetById(R Id);

    public abstract Task<T?> GetByIdAsync(R Id);
}
