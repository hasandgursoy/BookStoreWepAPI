using FluentValidation;

namespace Webapi.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty().NotNull();
            
        }
    }
}