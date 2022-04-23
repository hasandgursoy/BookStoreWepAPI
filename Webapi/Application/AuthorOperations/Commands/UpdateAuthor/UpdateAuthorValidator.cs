using FluentValidation;

namespace Webapi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorValidator:AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorValidator()
        {
            RuleFor(command=> command.Model.Name).MinimumLength(3);
            RuleFor(command => command.Model.SurName).MinimumLength(3);
        }
    }
}