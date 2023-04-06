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
        public async Task<IActionResult> GetAllBooks()
        {
            var booksOperation = await bookService.GetAllBooksAsync();

            if (!booksOperation.IsSuccess)
            {
                return BadRequest(booksOperation.Errors);
            }
            else
            {
                return Ok(booksOperation.Entity);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetBookByTitle(string title)
        {
            var bookOperation = await bookService.GetBookByTitleAsync(title);

            if (!bookOperation.IsSuccess)
            {
                return BadRequest(bookOperation.Errors);
            }

            return Ok(bookOperation.Entity);
        }


    }
}
