using FluentValidation;

namespace Webapi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorValidator:AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorValidator(){
            RuleFor(command => command.Model.Name).MinimumLength(3);
            RuleFor(command => command.Model.SurName).MinimumLength(3);            
        }
    }
}