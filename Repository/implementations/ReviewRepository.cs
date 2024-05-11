
using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class ReviewRepository(DbContext dbContext) : Repository<Review, long>(dbContext), IReviewRepository
{
    public async Task<List<Review>> GetActiveForUser(string Id)
    {
        return await _dbContext.Reviews
            .Where(review => review.UserId.Equals(Id))
            .Where(review => review.IsActive)
            .ToListAsync();
    }

    public override Review? GetById(long Id)
    {
        return _dbContext.Reviews.FirstOrDefault(review => review.Id == Id);
    }

    public async override Task<Review?> GetByIdAsync(long Id)
    {
        return await _dbContext.Reviews.FirstOrDefaultAsync(review => review.Id == Id);
    }

    public async Task<List<Review>> GetCompletedForUser(string Id)
    {
         return await _dbContext.Reviews
            .Where(review => review.UserId.Equals(Id))
            .Where(review => !review.IsActive)
            .ToListAsync();
    }
}
