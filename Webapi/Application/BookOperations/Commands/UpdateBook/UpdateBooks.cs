using AutoMapper;
using Webapi.DBOperations;
using WebApi;
using WebApi.DBOperations;

namespace Webapi.BookOperations.UpdateBook
{
    
    public class UpdateBooks{


        private readonly IBookStoreDBContext _dbContext;
        private readonly IMapper _mapper;
        public int ID;
        public UpdateBookModels updateBookModels{get;set;}

        public UpdateBooks(IBookStoreDBContext dbContext, IMapper mapper, UpdateBookModels updateBookModels)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this.updateBookModels = updateBookModels;
        }

        public void Handle(){

            var book = _dbContext.Books.SingleOrDefault(x => x.ID == ID);

            if(book is null){
                throw new InvalidOperationException("Güncellemek istediğiniz kitap bulunamadı.");
            }

            var nes = _mapper.Map<Book>(updateBookModels);

            book.Title = nes.Title;
            book.GenreId = nes.GenreId;
            book.PublisDate = nes.PublisDate;

            _dbContext.SaveChanges();

        }

    }

    public class UpdateBookModels
    {   
        
        public string? Title { get; set; }
        public int PageCount { get; set; }
        public  DateTime PublisDate {get;set;}
        public int GenreId {get;set;}


    }

}