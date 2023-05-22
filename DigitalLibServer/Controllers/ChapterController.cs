using BLL.Interfaces;
using BLL.Model.Chapter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private IChapterService _chapterService;

        public ChapterController(IChapterService chapterService) 
        {
            _chapterService = chapterService;
        }

        [HttpGet("{chapterId}")]
        [Authorize]
        public async Task<IActionResult> GetChapterAsync(int chapterId)
        {
            var chapterOperation = await _chapterService.GetChapterAsync(chapterId);

            if(!chapterOperation.IsSuccess)
            {
                return BadRequest(new {errors =  chapterOperation.Errors});
            }

            return Ok(new { data = chapterOperation.Entity });
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> GetChapterListAsync(int bookId)
        {
            var chapterOperation = await _chapterService.GetChapterListAsync(bookId);

            if (!chapterOperation.IsSuccess)
            {
                return BadRequest(new { errors = chapterOperation.Errors });
            }

            return Ok(new { data = chapterOperation.Entity });
        }

        [HttpDelete("delete/{chapterId}")]
        [Authorize]
        public async Task<IActionResult> DeleteChapterAsync(int chapterId)
        {
            var chapterOperation = await _chapterService.DeleteChapterAsync(chapterId);

            if (!chapterOperation.IsSuccess)
            {
                return BadRequest(new { errors = chapterOperation.Errors });
            }

            return Ok(new { data = chapterOperation.Entity });
        }

        [HttpPut("update/{chapterId}")]
        [Authorize]
        public async Task<IActionResult> UpdateChapterAsync(int chapterId, ChapterModel chapter)
        {
            var chapterOperation = await _chapterService.UpdateChapterAsync(chapterId, chapter);

            if (!chapterOperation.IsSuccess)
            {
                return BadRequest(new { errors = chapterOperation.Errors });
            }

            return Ok(new { data = chapterOperation.Entity });
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> CreateChapterAsync(ChapterModel chapter)
        {
            var chapterOperation = await _chapterService.CreateChapterAsync(chapter);

            if (!chapterOperation.IsSuccess)
            {
                return BadRequest(new { errors = chapterOperation.Errors });
            }

            return Ok(new { data = chapterOperation.Entity });
        }
    }
}
