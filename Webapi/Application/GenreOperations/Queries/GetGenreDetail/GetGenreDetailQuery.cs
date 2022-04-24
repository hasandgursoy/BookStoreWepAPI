using AutoMapper;
using Webapi.DBOperations;
using WebApi.DBOperations;

namespace Webapi.Application.GenreOperations.Queries.GetGenresQuery
{
    public class GetGenreDetailQuery
    {   
        public int GenreId {get;set;}
        public readonly IBookStoreDBContext _context;
        public readonly IMapper _mapper;
        public GetGenreDetailQuery(IBookStoreDBContext context,IMapper mapper )
        {
            _context = context;
            _mapper = mapper;

        }

        public GenresDetailViewModel Handle(){
            
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);
            if (genre is null)
            {   
                throw new InvalidOperationException("Kitap türü bulunamadı.");
            }

            GenresDetailViewModel retrunObj = _mapper.Map<GenresDetailViewModel>(genre);

            return retrunObj;

        }
    }


    public class GenresDetailViewModel{
        public int Id { get; set; }
        public string? Name {get;set;}


    }
    
           
}