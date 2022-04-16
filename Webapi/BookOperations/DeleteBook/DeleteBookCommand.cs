using WebApi.DBOperations;

namespace Webapi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDBContext _dbContext;
        public int _id {get;set;} 

        public DeleteBookCommand(BookStoreDBContext dBContext, int id){
            this._dbContext = dBContext;
            this._id = id;
        }

        public void Handle(){
            
            var book = _dbContext.Books.SingleOrDefault(x => x.ID == _id);
            if(book is null){
                throw new InvalidOperationException("Olmayan id girdiniiz !");
            }

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();

            
        }        

    }

    // public class DeleteBookModel {

    //     public string? Title { get; set; }
    //     public int PageCount { get; set; }
    //     public string? GenreID { get; set; }
    //     public string? PublishDate { get; set; }
    // }

}