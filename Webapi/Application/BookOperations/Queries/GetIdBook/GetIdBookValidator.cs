using FluentValidation;

namespace Webapi.BookOperations.GetIdBook
{
    
    public class GetIdBookValidator : AbstractValidator<GetIdBook>
    {
        
        public GetIdBookValidator(){

            RuleFor(command => command.bookID).GreaterThan(0);

        }

    }

}