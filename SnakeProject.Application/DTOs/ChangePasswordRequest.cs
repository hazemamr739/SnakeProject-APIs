namespace SnakeProject.Application.DTOs
{
    public record ChangePasswordRequest
   (
       string CurrentPassword,
       string NewPassword
   );
}
