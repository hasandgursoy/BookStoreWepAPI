using AutoMapper;
using Webapi.DBOperations;
using Webapi.Entities;
using WebApi.DBOperations;

namespace Webapi.Application.AuthorOperations.Queries.GetAuthorDetailQuery
{
    public class GetAuthorDetailQuery
    {
        private readonly IBookStoreDBContext _context;
        private readonly IMapper _mapper;
        public int AuthorID { get; set; }
        public GetAuthorDetailQuery(IBookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailQueryModel Handle()
        {

            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorID);

            if (author is null)
            {
                throw new InvalidOperationException("Yazar Bulunamadı.");
            }

            AuthorDetailQueryModel query = _mapper.Map<AuthorDetailQueryModel>(author);
            return query;

        }
        

    }

    public class AuthorDetailQueryModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? DateOfBirth { get; set; }
        public bool IsBookPublished {get;set;} = true;
    }
}