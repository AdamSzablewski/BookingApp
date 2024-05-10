
using Microsoft.EntityFrameworkCore;

namespace BookingApp;

public class ReviewRepository(DbContext dbContext) : Repository<Review, long>(dbContext), IReviewRepository
{
    
    public override Review? GetById(long Id)
    {
        return _dbContext.Reviews.FirstOrDefault(review => review.Id == Id);
    }

    public async override Task<Review?> GetByIdAsync(long Id)
    {
        return await _dbContext.Reviews.FirstOrDefaultAsync(review => review.Id == Id);

    }
}
