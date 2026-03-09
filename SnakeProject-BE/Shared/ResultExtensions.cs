using Microsoft.AspNetCore.Mvc;

namespace SnakeProject_BE.Shared
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult(this Result result) => result switch
        {
            Result.Success success => new OkObjectResult(new { data = success.Data, success = true }),
            Result.Failure failure => new BadRequestObjectResult(new { message = failure.Message, success = false }),
            _ => new StatusCodeResult(StatusCodes.Status500InternalServerError)
        };

        public static IActionResult ToCreatedAtActionResult(this Result result, ControllerBase controller, string actionName, object? routeValues, object? data = null) => result switch
        {
            Result.Success success => new CreatedAtActionResult(actionName, controller.ControllerContext.ActionDescriptor.ControllerName, routeValues, success.Data ?? data),
            Result.Failure failure => new BadRequestObjectResult(new { message = failure.Message, success = false }),
            _ => new StatusCodeResult(StatusCodes.Status500InternalServerError)
        };
    }
}
