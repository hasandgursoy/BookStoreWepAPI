using FluentValidation;

namespace Webapi.BookOperations.DeleteBook
{
    public class DeleteBookCommandValidator:AbstractValidator<DeleteBookCommand>

    {
        public DeleteBookCommandValidator(){

            RuleFor(command => command._id).GreaterThan(0);
            
        }

    }
}