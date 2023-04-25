using BLL.Model;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;

namespace DigitalLibServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;

        public BookController(IBookService _bookService) 
        {
            bookService = _bookService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllBooksInfo()
        {
            var booksOperation = await bookService.GetAllBooksInfoAsync();

            if (!booksOperation.IsSuccess)
            {
                return BadRequest(new {errors = booksOperation.Errors });
            }
            else
            {
                return Ok(new {data = booksOperation.Entity});
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetBookById(int bookId)
        {
            var booksOperation = await bookService.GetBookByIdAsync(bookId);

            if (!booksOperation.IsSuccess)
            {
                return BadRequest(new { errors = booksOperation.Errors });
            }
            else
            {
                return Ok(new {data = booksOperation.Entity});
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetBookByTitle(string title)
        {
            var bookOperation = await bookService.GetBookByTitleAsync(title);

            if (!bookOperation.IsSuccess)
            {
                return BadRequest(new {errors = bookOperation.Errors});
            }

            return Ok(new { data = bookOperation.Entity });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetImage(int bookId)
        {
            var imageOperation = await bookService.GetImageAsync(bookId);

            if (!imageOperation.IsSuccess)
            {
                return BadRequest(new { errors = imageOperation.Errors });
            }

            return Ok(new { data = imageOperation.Entity });
        }


    }
}
