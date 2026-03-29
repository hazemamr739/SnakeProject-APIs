using SnakeProject.Application.DTOs.Auth;

namespace SnakeProject.Application.Validators.Auth
{
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.Token).NotEmpty();

            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
