using SnakeProject.Application.DTOs;

namespace SnakeProject.Application.Validators.User
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(p => p.CurrentPassword)
                .NotEmpty();

            RuleFor(p => p.NewPassword)
                .NotEmpty()
                .Matches(RegexPatterns.Password)
                .WithMessage("Password must be at least 8 digits  and contain at least one uppercase letter, one lowercase letter, one digit and one special character ")
                .NotEqual(p => p.CurrentPassword)
                .WithMessage("New Password cannot be same as the current password");

        }
    }
}
