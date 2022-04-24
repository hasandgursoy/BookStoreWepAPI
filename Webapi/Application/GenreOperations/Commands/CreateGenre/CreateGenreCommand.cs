using Webapi.DBOperations;
using Webapi.Entities;
using WebApi.DBOperations;

namespace Webapi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {   
        private readonly IBookStoreDBContext _context;
        public CreateGenreModel Model {get;set;}
        public CreateGenreCommand(IBookStoreDBContext context)
        {
            _context = context;
            
        }

        public void Handle(){
            var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if(genre is not null){
                throw new InvalidOperationException("Kitap Türü Zaten Mevcut");
            }
            genre = new Genre();
            genre.Name = Model.Name;
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

    }

    public class CreateGenreModel
    {
        public string? Name { get; set; }
    }
}