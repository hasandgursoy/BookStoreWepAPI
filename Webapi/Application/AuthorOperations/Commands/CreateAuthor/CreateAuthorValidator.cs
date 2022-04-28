using FluentValidation;

namespace Webapi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorValidator:AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorValidator(){
            RuleFor(command => command.Model.Name).NotEmpty().NotNull().MinimumLength(2);
            RuleFor(command => command.Model.SurName).NotEmpty().NotNull().MinimumLength(2);
            RuleFor(command => command.Model.DateOfBirth).NotEmpty().NotNull().LessThan(DateTime.Now);

        }
    }
}