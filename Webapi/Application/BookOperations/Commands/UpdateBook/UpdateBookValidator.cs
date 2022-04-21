using FluentValidation;

namespace Webapi.BookOperations.UpdateBook
{
    
    public class UpdateBookValidator : AbstractValidator<UpdateBooks>
    {
        public UpdateBookValidator(){

            RuleFor(command => command.ID).GreaterThan(0);
        }
    }

}