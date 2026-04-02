namespace SnakeProject.Application.DTOs.Auth
{
    public record ResetPasswordRequest (
       string Email,
         string ResetCode,
         string NewPassword
   );
}
