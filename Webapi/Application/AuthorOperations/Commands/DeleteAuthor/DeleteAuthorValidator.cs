using FluentValidation;

namespace Webapi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorValidator:AbstractValidator<DeleteAuthorCommand>
    {

        public DeleteAuthorValidator(){
            RuleFor(command => command.AuthorID).GreaterThan(0);
            
        }

    }
}