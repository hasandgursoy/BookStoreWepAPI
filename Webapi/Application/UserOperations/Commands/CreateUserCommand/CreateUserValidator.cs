using FluentValidation;

namespace Webapi.Application.UserOperations.Commands.CreateUserCommand
{

    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {

            RuleFor(x => x.Model.Name).NotEmpty().NotNull().MinimumLength(2);
            RuleFor(x => x.Model.Surname).NotEmpty().NotNull().MinimumLength(2);
            RuleFor(x => x.Model.Email).NotEmpty().NotNull().MinimumLength(7);
            RuleFor(x => x.Model.Password).NotEmpty().NotNull().MinimumLength(7);
            
        }
    }
}