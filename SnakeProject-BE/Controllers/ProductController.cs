using Microsoft.AspNetCore.Mvc;
using SnakeProject.Application.DTOs.Product;
namespace SnakeProject_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IPsnCodeService _psnCodeService) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<PsnCodeResponse>> GetAll()
        {
            var result = await _psnCodeService.GetAllPsnCodesAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PsnCodeResponse>> Create([FromBody] PsnCodeRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                await _psnCodeService.AddPsnCodeAsync(request, cancellationToken);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPsnCodeById(int id)
        {
            await _psnCodeService.GetPsnCodeByIdAsync(id);
            return NoContent();
        }
    }
}
