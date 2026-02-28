using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SnakeProject_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            

            return Ok();

        }
    }
}
