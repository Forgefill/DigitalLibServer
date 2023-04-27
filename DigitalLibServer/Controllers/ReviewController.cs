using BLL.Interfaces;
using BLL.Model.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibServer.Controllers
{
    [Route("api/Book/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private IReviewService _reviewService;

        public ReviewController(IReviewService reviewService) 
        {
            _reviewService = reviewService;
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> GetReviewListAsync(int bookId)
        {
            var reviewOperation = await _reviewService.GetReviewListAsync(bookId);

            if(!reviewOperation.IsSuccess)
            {
                return BadRequest(new {errors = reviewOperation.Errors});
            }

            return Ok(new { data = reviewOperation.Entity });
        }

        [HttpDelete("delete/{reviewId}")]
        [Authorize]
        public async Task<IActionResult> DeleteReviewAsync(int reviewId)
        {
            var reviewOperation = await _reviewService.DeleteReviewAsync(reviewId);

            if (!reviewOperation.IsSuccess)
            {
                return BadRequest(new { errors = reviewOperation.Errors });
            }

            return Ok(new { data = reviewOperation.Entity });
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateReviewAsync(ReviewModel review)
        {
            var reviewOperation = await _reviewService.CreateReviewAsync(review);

            if (!reviewOperation.IsSuccess)
            {
                return BadRequest(new { errors = reviewOperation.Errors });
            }

            return Ok(new { data = reviewOperation.Entity });
        }

        [HttpPut("update/{reviewId}")]
        [Authorize]
        public async Task<IActionResult> DeleteReviewAsync(int reviewId, ReviewModel reviewModel)
        {
            var reviewOperation = await _reviewService.UpdateReviewAsync(reviewId, reviewModel);

            if (!reviewOperation.IsSuccess)
            {
                return BadRequest(new { errors = reviewOperation.Errors });
            }

            return Ok(new { data = reviewOperation.Entity });
        }
    }
}
