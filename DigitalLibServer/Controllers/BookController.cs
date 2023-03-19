using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpGet]
        [Route("getString")]
        [Authorize(Roles = "Moderator")]
        public ActionResult getString()
        {
            return Ok("You are admin");
        }
    }
}
