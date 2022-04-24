using AutoMapper;
using Webapi.DBOperations;
using WebApi.DBOperations;

namespace Webapi.Application.GenreOperations.Queries.GetGenresQuery
{
    public class GetGenresQuery
    {
        public readonly IBookStoreDBContext _context;
        public readonly IMapper _mapper;
        public GetGenresQuery(IBookStoreDBContext context,IMapper mapper )
        {
            _context = context;
            _mapper = mapper;

        }

        public List<GenresViewModel> Handle(){
            
            var genres = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);
            List<GenresViewModel> retrunObj = _mapper.Map<List<GenresViewModel>>(genres);

            return retrunObj;

        }
    }


    public class GenresViewModel{
        public int Id { get; set; }
        public string? Name {get;set;}


    }
    
           
}