using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace Webapi.Application.AuthorOperations.Queries.GetAuthorsQuery
{
    
    public class GetAuthorsQuery
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsQueryModel> Handle(){

            var authors = _context.Authors.OrderBy(x => x.Id).ToList();
            List<AuthorsQueryModel> returnObj = _mapper.Map<List<AuthorsQueryModel>>(authors);

            return returnObj;
            

        }
    }

    public class AuthorsQueryModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? DateOfBirth  { get; set; }
    }

}