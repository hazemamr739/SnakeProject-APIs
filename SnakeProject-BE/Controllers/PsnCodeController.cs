using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnakeProject.API.Authentication;
using SnakeProject.Domain.Enums;

namespace SnakeProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PsnCodeController(IPsnCodeService psnCodeService) : ControllerBase
{
    private readonly IPsnCodeService _psnCodeService = psnCodeService;

    [HasPermission(Permissions.PsnCodesRead)]
    [HttpGet]
    public async Task<IActionResult> GetAllPsnCodes([FromQuery] InventoryStatus? status, CancellationToken cancellationToken)
    {
        var result = await _psnCodeService.GetAllPsnCodeAsync(status, cancellationToken);
        return Ok(result);
    }
    [Authorize]
    [HasPermission(Permissions.PsnCodesRead)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _psnCodeService.GetPsnCodeAsync(id, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : Problem(statusCode: StatusCodes.Status404NotFound, title: result.Error.Description);
    }

    [Authorize]
    [HasPermission(Permissions.PsnCodesInventorySummary)]
    [HttpGet("denominations/{denominationId:int}/summary")]
    public async Task<IActionResult> GetInventorySummary(int denominationId, CancellationToken cancellationToken)
    {
        var result = await _psnCodeService.GetInventorySummaryAsync(denominationId, cancellationToken);
        return Ok(result);
    }

    [Authorize]
    [HasPermission(Permissions.PsnCodesAdd)]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] PsnCodeRequest request, CancellationToken cancellationToken)
    {
        var result = await _psnCodeService.AddAsyn(request, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
            : Problem(statusCode: StatusCodes.Status400BadRequest, title: result.Error.Description);
    }


    [Authorize]
    [HasPermission(Permissions.PsnCodesUpdate)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PsnCodeRequest request, CancellationToken cancellationToken)
    {
        var result = await _psnCodeService.UpdateAsyn(id, request, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : Problem(statusCode: StatusCodes.Status404NotFound, title: result.Error.Description);
    }


    [Authorize]
    [HasPermission(Permissions.PsnCodesReserve)]
    [HttpPost("{id:int}/reserve")]
    public async Task<IActionResult> Reserve(int id, CancellationToken cancellationToken)
    {
        var result = await _psnCodeService.ReserveAsync(id, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status409Conflict, title: result.Error.Description);
    }

    [Authorize]
    [HasPermission(Permissions.PsnCodesReserve)]
    [HttpPost("denominations/{denominationId:int}/reserve-next")]
    public async Task<IActionResult> ReserveNext(int denominationId, CancellationToken cancellationToken)
    {
        var result = await _psnCodeService.ReserveNextAvailableAsync(denominationId, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status409Conflict, title: result.Error.Description);
    }

    [Authorize]
    [HasPermission(Permissions.PsnCodesRelease)]
    [HttpPost("{id:int}/release")]
    public async Task<IActionResult> Release(int id, CancellationToken cancellationToken)
    {
        var result = await _psnCodeService.ReleaseAsync(id, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status409Conflict, title: result.Error.Description);
    }


    [Authorize]
    [HasPermission(Permissions.PsnCodesSell)]
    [HttpPost("{id:int}/sell")]
    public async Task<IActionResult> MarkSold(int id, CancellationToken cancellationToken)
    {
        var result = await _psnCodeService.MarkSoldAsync(id, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : Problem(statusCode: result.Error.StatusCode ?? StatusCodes.Status409Conflict, title: result.Error.Description);
    }


    [Authorize]
    [HasPermission(Permissions.PsnCodesDelete)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _psnCodeService.DeleteAsyn(id, cancellationToken);
        return result.IsSuccess
            ? NoContent()
            : Problem(statusCode: StatusCodes.Status404NotFound, title: result.Error.Description);
    }
}
