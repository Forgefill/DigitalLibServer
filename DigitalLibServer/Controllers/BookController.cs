using BLL.Model;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.Model.Book;

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

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> GetBookList()
        {
            var booksOperation = await bookService.GetBookListAsync();

            if (!booksOperation.IsSuccess)
            {
                return BadRequest(new {errors = booksOperation.Errors });
            }
            else
            {
                return Ok(new {data = booksOperation.Entity});
            }
        }

        [HttpGet("/{bookId}")]
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

        [HttpGet("/getByTitle")]
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

        [HttpGet("/image/{bookId}")]
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

        [HttpDelete("/delete/{bookId}")]
        [Authorize]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var bookOperation = await bookService.DeleteBookAsync(bookId);

            if (!bookOperation.IsSuccess)
            {
                return BadRequest(new { errors = bookOperation.Errors });
            }

            return Ok();
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> CreateBook(CreateBookModel createBookModel)
        {
            var bookOperation = await bookService.CreateBookAsync(createBookModel);

            if (!bookOperation.IsSuccess)
            {
                return BadRequest(new { errors = bookOperation.Errors });
            }

            return Ok(new {data = bookOperation.Entity});
        }

        [HttpPut("update/{bookId}")]
        [Authorize]
        public async Task<IActionResult> UpdateBook(int bookId, UpdateBookModel updateBookModel)
        {
            var bookOperation = await bookService.UpdateBookAsync(bookId, updateBookModel);

            if (!bookOperation.IsSuccess)
            {
                return BadRequest(new {errors = bookOperation.Errors});
            }

            return Ok(new { data = bookOperation.Entity });
        }
    }
}
