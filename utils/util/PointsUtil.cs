
using BookingApp;

public static class PointsUtil
{
    internal static int GetScore(List<Review> reviews)
    {
        Console.WriteLine("pointsss   ------ "+reviews.Count);
        int sum = 0;
        foreach (Review review in reviews)
        {
            sum += review.Points;
        }
        if(reviews.Count == 0)
        {
            return 0;
        }
        return sum / reviews.Count;
    }
}