using FluentValidation;

namespace Webapi.Application.AuthorOperations.Queries.GetAuthorDetailQuery
{
    public class GetAuthorDetailValidator:AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailValidator(){
            RuleFor(query => query.AuthorID).GreaterThan(0);
        }
    }
}