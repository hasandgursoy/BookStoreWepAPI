using Webapi.Common;
using WebApi;
using WebApi.DBOperations;

namespace Webapi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {

        private readonly BookStoreDBContext _dbContext;

        public GetBooksQuery(BookStoreDBContext dBContext)
        {
            this._dbContext = dBContext;
        }

        public List<BooksViewModel> Handle()
        {

            var bookList = _dbContext.Books.OrderBy(x => x.ID).ToList<Book>();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach (var book in bookList)
            {
                vm.Add(new BooksViewModel()
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PublisDate = book.PublisDate.Date.ToString("dd/MM/yyyy"),
                    PageCount = book.PageCount
                });
            }
            return vm;

        }

    }

    public class BooksViewModel
    {

        public string? Title { get; set; }
        public int PageCount { get; set; }
        public string? PublisDate { get; set; }
        public string? Genre { get; set; }
    }

}