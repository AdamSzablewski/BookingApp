namespace BookingApp;

public interface IReviewRepository : IRepository<Review, long>
{
    public Task<List<Review>> GetActiveForUser(string Id);
    public Task<List<Review>> GetCompletedForUser(string id);
}
