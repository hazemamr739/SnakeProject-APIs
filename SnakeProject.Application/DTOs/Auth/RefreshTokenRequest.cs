namespace SnakeProject.Application.DTOs.Auth
{
    public record RefreshTokenRequest(
      string Token,
      string RefreshToken
    );
}
