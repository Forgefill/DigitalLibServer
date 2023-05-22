using BLL.Interfaces;
using BLL.Model.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> GetCommentListAsync(int chapterId)
        {
            var commentOperation = await _commentService.GetCommentListAsync(chapterId);

            if (!commentOperation.IsSuccess)
            {
                return BadRequest(new { errors = commentOperation.Errors });
            }

            return Ok(new { data = commentOperation.Entity });
        }

        [HttpDelete("delete/{commentId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCommentAsync(int commentId)
        {
            var commentOperation = await _commentService.DeleteCommentAsync(commentId);

            if (!commentOperation.IsSuccess)
            {
                return BadRequest(new { errors = commentOperation.Errors });
            }

            return Ok(new { data = commentOperation.Entity });
        }

        [HttpPut("update/{commentId}")]
        [Authorize]
        public async Task<IActionResult> UpdateCommentAsync(int commentId, CommentModel comment)
        {
            var commentOperation = await _commentService.UpdateCommentAsync(commentId, comment);

            if (!commentOperation.IsSuccess)
            {
                return BadRequest(new { errors = commentOperation.Errors });
            }

            return Ok(new { data = commentOperation.Entity });
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> CreateCommentAsync(CommentModel comment)
        {
            var commentOperation = await _commentService.CreateCommentAsync(comment);

            if (!commentOperation.IsSuccess)
            {
                return BadRequest(new { errors = commentOperation.Errors });
            }

            return Ok(new { data = commentOperation.Entity });
        }
    }
}
