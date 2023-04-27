using BLL.Interfaces;
using BLL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace DigitalLibServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private IGenreService _genreService;

        public GenreController(IGenreService genreService) 
        {
            _genreService = genreService;
        }

        [HttpGet("{genreId}")]
        [Authorize]
        public async Task<IActionResult> GetGenre(int genreId)
        {
            var genreOperation = await _genreService.GetGenreByIdAsync(genreId);

            if (!genreOperation.IsSuccess)
            {
                return BadRequest(new { errors = genreOperation.Errors });
            }

            return Ok(new { data = genreOperation.Entity });
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> GetGenres()
        {
            var genreOperation = await _genreService.GetGenreListAsync();

            if (!genreOperation.IsSuccess)
            {
                return BadRequest(new { errors = genreOperation.Errors });
            }

            return Ok(new { data = genreOperation.Entity });
        }

        [HttpPost("add")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> AddGenre(GenreModel genre)
        {
            var genreOperation = await _genreService.CreateGenreAsync(genre);

            if (!genreOperation.IsSuccess)
            {
                return BadRequest(new { errors = genreOperation.Errors });
            }
            
            return Ok(genre);
        }

        [HttpDelete("delete/{genreId}")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> DeleteGenre(int genreId)
        {
            var genreOperation = await _genreService.DeleteGenreAsync(genreId);

            if (!genreOperation.IsSuccess)
            {
                return BadRequest(new { errors = genreOperation.Errors });
            }

            return Ok();
        }

        [HttpPut("update/{genreId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateGenre(int genreId, GenreModel genre)
        {
            var genreOperation = await _genreService.UpdateGenreAsync(genreId, genre);

            if (!genreOperation.IsSuccess)
            {
                return BadRequest(new { errors = genreOperation.Errors });
            }
            
            return Ok(new {data = genreOperation.Entity});
        }
    }
}
