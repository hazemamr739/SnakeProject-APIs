using SnakeProject.Application.DTOs.Auth;

namespace SnakeProject.Application.Validators.Auth
{
    public class ForgetPasswordRequestValidator : AbstractValidator<ForgetPasswordRequest>
    {
        public ForgetPasswordRequestValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
