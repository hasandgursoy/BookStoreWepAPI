using Webapi.DBOperations;
using WebApi.DBOperations;

namespace Webapi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {

        private readonly IBookStoreDBContext _context;
        public int GenreId {get;set;}

        public DeleteGenreCommand(IBookStoreDBContext context)
        {
            
            _context = context;
        }

        public void Handle(){

            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if(genre is null){
                throw new InvalidOperationException("Kitap Türü bulunamadı.");
            }

            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }


    }
}