using WebApi.DBOperations;

namespace Webapi.BookOperations.UpdateBook
{
    
    public class UpdateBooks{


        private readonly BookStoreDBContext _dbContext;
        public int ID;
        public UpdateBookModels updateBookModels{get;set;}

        public UpdateBooks(BookStoreDBContext dbContext){
            this._dbContext = dbContext;
        }

        public void Handle(){

            var book = _dbContext.Books.SingleOrDefault(x => x.ID == ID);

            if(book is null){
                throw new InvalidOperationException("Güncellemek istediğiniz kitap bulunamadı.");
            }

            book.Title = book.Title != updateBookModels.Title ? updateBookModels.Title:book.Title;
            book.PageCount = book.PageCount != updateBookModels.PageCount ? updateBookModels.PageCount: book.PageCount;
            book.PublisDate = book.PublisDate != updateBookModels.PublisDate ? updateBookModels.PublisDate : book.PublisDate;
            book.GenreId = book.GenreId != updateBookModels.GenreId ? updateBookModels.GenreId : book.GenreId;

            _dbContext.SaveChanges();

        }

    }

    public class UpdateBookModels
    {   
        
        public string? Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublisDate {get;set;}
        public int GenreId {get;set;}


    }

}