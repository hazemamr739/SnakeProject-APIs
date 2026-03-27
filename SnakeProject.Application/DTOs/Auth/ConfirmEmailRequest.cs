
namespace SnakeProject.Application.DTOs.Auth
{
    public record ConfirmEmailRequest(
       string UserId,
       string Code
    );
}
