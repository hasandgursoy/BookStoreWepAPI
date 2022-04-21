using FluentValidation;
using Webapi.Application.GenreOperations.Queries.GetGenresQuery;

namespace Webapi.Application.BookOperations.Queries.GetIdBook
{
    public class GetGenreDetailQueryValidator:AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator(){
            
            RuleFor(query => query.GenreId).GreaterThan(0);
        }
    }
}