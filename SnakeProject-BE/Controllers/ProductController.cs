using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakeProject_BE.Contracts.Product;

namespace SnakeProject_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IPsnCodeService _psnCodeService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {


            return Ok();

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]PsnCodeRequest request, CancellationToken cancellationToken=default)
        {
            var result = _psnCodeService.AddPsnCodeAsync(request, cancellationToken = default);

            return Ok();
        }
    }
}
