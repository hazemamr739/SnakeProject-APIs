using Microsoft.AspNetCore.Mvc;

namespace SnakeProject_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PsnCodeController(IPsnCodeService psnCodeService) : ControllerBase
    {
        private readonly IPsnCodeService _psnCodeService = psnCodeService;

        [HttpGet]
        public async Task<IActionResult> GetAllPsnCodes(CancellationToken cancellationToken)
        {
            var result = await _psnCodeService.GetAllPsnCodeAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await _psnCodeService.GetPsnCodeAsync(id, cancellationToken);
            return result.IsSuccess
                ? Ok(result.Value)
                : Problem(statusCode: StatusCodes.Status404NotFound, title: result.Error.Description);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PsnCodeRequest request, CancellationToken cancellationToken)
        {
            var result = await _psnCodeService.AddAsyn(request, cancellationToken);
            return result.IsSuccess
                ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
                : Problem(statusCode: StatusCodes.Status400BadRequest, title: result.Error.Description);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] PsnCodeRequest request, CancellationToken cancellationToken)
        {
            var result = await _psnCodeService.UpdateAsyn(id, request, cancellationToken);
            return result.IsSuccess
                ? Ok(result.Value)
                : Problem(statusCode: StatusCodes.Status404NotFound, title: result.Error.Description);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            var result = await _psnCodeService.DeleteAsyn(id, cancellationToken);
            return result.IsSuccess
                ? NoContent()
                : Problem(statusCode: StatusCodes.Status404NotFound, title: result.Error.Description);
        }
    }
}
