﻿namespace BookingApp;

public class ReviewService(IReviewRepository reviewRepository, IFacilityRepository facilityRepository)
{
    private readonly IReviewRepository _reviewRepository = reviewRepository;
    private readonly IFacilityRepository _facilityRepository = facilityRepository;

    public async Task<List<Review>> GetActiveReviewsForUser(string Id)
    {
        List<Review> activeReviews = await _reviewRepository.GetActiveForUser(Id);
        return activeReviews;
    }
    public async Task<List<Review>> GetCompletedReviewsForUser(string Id)
    {
        List<Review> activeReviews = await _reviewRepository.GetCompletedForUser(Id);
        return activeReviews;
    }
    public async Task PostReview(ReviewCreateDto reviewCreateDto)
    {
        Review review = await _reviewRepository.GetByIdAsync(reviewCreateDto.Id) ?? throw new ReviewNotFoundException();
        Facility facility = await _facilityRepository.GetByIdAsync(reviewCreateDto.FacilityId) ?? throw new FacilityNotFoundException();
        int pointsGiven = reviewCreateDto.Points;
        if(reviewCreateDto.Text != null && reviewCreateDto.Text.Length > 0){
            review.Text = reviewCreateDto.Text;
            facility.Reviews.Add(review);   
        }
        Dictionary<int, int> points = facility.Points;
        points[pointsGiven]++;
        review.Points = pointsGiven;
        await _reviewRepository.UpdateAsync();
    }
}
