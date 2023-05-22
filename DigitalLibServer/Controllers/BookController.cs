using BLL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.Model.Book;

namespace DigitalLibServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IImageService _imageService;

        public BookController(IBookService bookService, IImageService imageService)
        {
            _bookService = bookService;
            _imageService = imageService;
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> GetBookList([FromQuery]BookFilters bookFilters, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var booksOperation = await _bookService.GetBookListAsync(bookFilters, page, pageSize);

            if (!booksOperation.IsSuccess)
            {
                return BadRequest(new { errors = booksOperation.Errors });
            }
            else
            {
                return Ok(new { data = booksOperation.Entity });
            }
        }

        [HttpGet("/{bookId}")]
        [Authorize]
        public async Task<IActionResult> GetBookById(int bookId)
        {
            var booksOperation = await _bookService.GetBookByIdAsync(bookId);

            if (!booksOperation.IsSuccess)
            {
                return BadRequest(new { errors = booksOperation.Errors });
            }
            else
            {
                return Ok(new { data = booksOperation.Entity });
            }
        }

        [HttpGet("/getByTitle")]
        [Authorize]
        public async Task<IActionResult> GetBookByTitle(string title)
        {
            var bookOperation = await _bookService.GetBookByTitleAsync(title);

            if (!bookOperation.IsSuccess)
            {
                return BadRequest(new { errors = bookOperation.Errors });
            }

            return Ok(new { data = bookOperation.Entity });
        }

        [HttpDelete("/delete/{bookId}")]
        [Authorize]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var bookOperation = await _bookService.DeleteBookAsync(bookId);

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
            var bookOperation = await _bookService.CreateBookAsync(createBookModel);

            if (!bookOperation.IsSuccess)
            {
                return BadRequest(new { errors = bookOperation.Errors });
            }

            return Ok(new { data = bookOperation.Entity });
        }

        [HttpPut("update/{bookId}")]
        [Authorize]
        public async Task<IActionResult> UpdateBook(int bookId, UpdateBookModel updateBookModel)
        {
            var bookOperation = await _bookService.UpdateBookAsync(bookId, updateBookModel);

            if (!bookOperation.IsSuccess)
            {
                return BadRequest(new { errors = bookOperation.Errors });
            }

            return Ok(new { data = bookOperation.Entity });
        }

        [HttpGet("/image")]
        [Authorize]
        public async Task<IActionResult> GetImage(int bookId)
        {
            var imageOperation = await _imageService.GetImageAsync(bookId);

            if (!imageOperation.IsSuccess)
            {
                return BadRequest(new { errors = imageOperation.Errors });
            }

            return Ok(new { data = imageOperation.Entity });
        }

        [HttpDelete("image/delete")]
        [Authorize]
        public async Task<IActionResult> RemoveImage(int bookId)
        {
            var imageOperation = await _imageService.DeleteImageAsync(bookId);

            if (!imageOperation.IsSuccess)
            {
                return BadRequest(new { errors = imageOperation.Errors });
            }

            return Ok(new { data = imageOperation.Entity });
        }

        [HttpPost("image/add")]
        [Authorize]
        public async Task<IActionResult> RemoveImage([FromForm] IFormFile imageFile, int bookId)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Image file is required");
            }
            else if (!imageFile.ContentType.StartsWith("image/"))
            {
                return BadRequest("Only image files are allowed");
            }

            byte[] imageData;
            using (var stream = new MemoryStream())
            {
                await imageFile.CopyToAsync(stream);
                imageData = stream.ToArray();
            }

            var image = new ImageModel
            {
                BookId = bookId,
                ContentType = imageFile.ContentType,
                ImageData = imageData
            };

            var imageOperation = await _imageService.CreateImageAsync(image);

            if (!imageOperation.IsSuccess)
            {
                return BadRequest(new { errors = imageOperation.Errors });
            }

            return Ok(new { data = imageOperation.Entity });
        }

        [HttpPost("image/update")]
        [Authorize]
        public async Task<IActionResult> UpdateImage([FromForm] IFormFile imageFile, int bookId)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Image file is required");
            }
            else if (!imageFile.ContentType.StartsWith("image/"))
            {
                return BadRequest("Only image files are allowed");
            }

            byte[] imageData;
            using (var stream = new MemoryStream())
            {
                await imageFile.CopyToAsync(stream);
                imageData = stream.ToArray();
            }

            var image = new ImageModel
            {
                BookId = bookId,
                ContentType = imageFile.ContentType,
                ImageData = imageData
            };

            var imageOperation = await _imageService.UpdateImageAsync(bookId, image);

            if (!imageOperation.IsSuccess)
            {
                return BadRequest(new { errors = imageOperation.Errors });
            }

            return Ok(new { data = imageOperation.Entity });
        }
    }
}
