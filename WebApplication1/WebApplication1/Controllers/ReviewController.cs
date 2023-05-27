using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO.Review;
using WebApplication1.Models;
using WebApplication1.Services.ReviewService;

namespace WebApplication1.Controllers
{
    [Route("api/review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {

        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("{id}")]
        public async Task<Review> GetReviewById(Guid id)
        {
            return await _reviewService.GetReview(id);
        }
        
        [HttpGet("by-book/{bookId}")]
        public async Task<List<BookReview>> GetReviewByBookId(Guid bookId)
        {
            return await _reviewService.GetReviewsByBookId(bookId);
        }

        [HttpGet("by-user/{userId}")]
        public async Task<List<UserReview>> GetReviewsByUserId(Guid userId)
        {
            return await _reviewService.GetReviewsByUserId(userId);
        }
        [Authorize]
        [HttpGet("my")]
        public async Task<List<UserReview>> GetMyReviews()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userIdGuid = Guid.Parse(userId);

            return await _reviewService.GetReviewsByUserId(userIdGuid);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<Review> CreateReview(ReviewForPost reviewForPost)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            reviewForPost.UserId = Guid.Parse(userId);

            return await _reviewService.CreateReview(reviewForPost);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task UpdateReview(Guid id, ReviewForUpdate reviewForUpdate)
        {
            await _reviewService.UpdateReview(id, reviewForUpdate);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task DeleteReview(Guid id)
        {
            await _reviewService.DeleteReview(id);

        }
    }
}
