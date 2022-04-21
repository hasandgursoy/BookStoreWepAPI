using WebApi.DBOperations;

namespace Webapi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {   
        public int GenreId  {get;set;}
        private readonly BookStoreDBContext _context;
        public UpdateGenreCommandModel Model{get;set;}
        public UpdateGenreCommand(BookStoreDBContext context, UpdateGenreCommandModel model)
        {
            _context = context;
            Model = model;
            
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if(genre is null){
                throw new  InvalidOperationException("Eşeleşen Genre ID Bulunamadı.");
            }
            
            if(_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId)){
                throw new InvalidOperationException("Aynı isimle bir kitap türü zaten mevcut");
            }

            genre.Name = Model.Name.Trim() != genre.Name  && Model.Name.Trim() != default ? Model.Name : genre.Name;
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }
    }

    public class UpdateGenreCommandModel
    {
        public string Name {get;set;}
        public bool IsActive{get;set;} = true;
        
    }


}