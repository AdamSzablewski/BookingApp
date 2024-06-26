﻿
namespace BookingApp;

public abstract class Repository<T, R>(DbContext dbContext) : IRepository<T, R>
{
    protected readonly DbContext _dbContext = dbContext;

    public T Create(T obj)
    {
        _dbContext.Add(obj);
        return obj;
    }
    public async Task<T> CreateAsync(T obj)
    {
        await _dbContext.AddAsync(obj);
        return obj;
    }
    public ICollection<T> CreateAll(ICollection<T> values)
    {
        _dbContext.AddRange(values);
        return  values;
    }
    public bool Delete(T obj)
    {
        var removed = _dbContext.Remove(obj);
        return removed != null;
    }
    public async Task<bool> DeleteAsync(T obj)
    {
        var removed = _dbContext.Remove(obj);
        return removed != null;
    }
    public bool Update()
    {
        _dbContext.SaveChanges();
        return true;
    }

    public async Task<bool> UpdateAsync()
    {
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public abstract T? GetById(R Id);

    public abstract Task<T?> GetByIdAsync(R Id);

    public async Task<ICollection<T>> CreateAllAsync(ICollection<T> values)
    {
        await _dbContext.AddRangeAsync(values);
        return  values;
    }
}
