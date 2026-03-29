using SnakeProject.Application.DTOs.Auth;

namespace SnakeProject.Application.Validators.Auth
{
    public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
    {
        public ConfirmEmailRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.Code).NotEmpty();

        }
    }
}
