using Microsoft.AspNetCore.Mvc;

namespace SnakeProject_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PsnCodeController(IPsnCodeService psnCodeService) : ControllerBase
    {
        private readonly IPsnCodeService _psnCodeService = psnCodeService;

        [HttpGet]
        public IActionResult GetAllPsnCodes(CancellationToken cancellationToken)
        {
            var result =  _psnCodeService.GetAllPsnCodeAsync(cancellationToken).Result;
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id,CancellationToken cancellationToken)
        {
            var result = _psnCodeService.GetPsnCodeAsync(id,cancellationToken).Result;

            return result.IsSuccess
                ? Ok(result.Value)
                : Problem(statusCode: StatusCodes.Status404NotFound, title: result.Error.Description);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]PsnCodeRequest request, CancellationToken cancellationToken)
        {
            var result = await psnCodeService.AddAsyn(request, cancellationToken);
           
            return result.IsSuccess
                ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
                : Problem(statusCode: StatusCodes.Status400BadRequest, title: result.Error.Description);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody]string id,[FromBody] PsnCodeRequest request, CancellationToken cancellationToken)
        {
            var result = await psnCodeService.UpdateAsyn(id, request, cancellationToken);
            return result.IsSuccess
                ? Ok(result.Value)
                : Problem(statusCode: StatusCodes.Status404NotFound, title: result.Error.Description);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            var result = await psnCodeService.DeleteAsyn(id, cancellationToken);
            return result.IsSuccess
                ? NoContent()
                : Problem(statusCode: StatusCodes.Status404NotFound, title: result.Error.Description);
        }
    }
}
