using WebApplication1.DTO.Review;
using WebApplication1.Models;

namespace WebApplication1.Services.ReviewService
{
    public interface IReviewService
    {
        Task<Review> GetReview(Guid id);
        Task<List<BookReview>> GetReviewsByBookId(Guid id);
        Task<List<UserReview>> GetReviewsByUserId(Guid id);
        Task<Review> CreateReview(ReviewForPost reviewForPost);
        Task UpdateReview(Guid id, ReviewForUpdate reviewForUpdate);
        Task DeleteReview(Guid id);
    }
}
