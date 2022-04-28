using FluentValidation;

namespace Webapi.Application.UserOperations.Commands.CreateToken
{

    public class CreateTokenValidator : AbstractValidator<CreateTokenCommand>
    {

        public CreateTokenValidator()
        {

            RuleFor(x => x.Model.Email).NotEmpty().NotNull().MinimumLength(7);
            RuleFor(x => x.Model.Password).NotEmpty().NotNull().MinimumLength(7);
            

        }

    }

}