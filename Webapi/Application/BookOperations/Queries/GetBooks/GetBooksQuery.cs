using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Webapi.Common;
using Webapi.DBOperations;
using WebApi;
using WebApi.DBOperations;

namespace Webapi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {

        private readonly IBookStoreDBContext _dbContext;
        private readonly IMapper _mapper;

        public GetBooksQuery(IBookStoreDBContext dBContext, IMapper mapper)
        {
            this._dbContext = dBContext;
            this._mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {

            var bookList = _dbContext.Books.Include(x => x.Genre).Include(x => x.Author).OrderBy(x => x.ID).ToList<Book>();
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);
            
            
            // foreach (var book in bookList)
            // {
            //     vm.Add(new BooksViewModel()
            //     {
            //         Title = book.Title,
            //         Genre = ((GenreEnum)book.GenreId).ToString(),
            //         PublisDate = book.PublisDate.Date.ToString("dd/MM/yyyy"),
            //         PageCount = book.PageCount
            //     });
            // }
            return vm;

        }

    }

    public class BooksViewModel
    {

        public string? Title { get; set; }
        public int PageCount { get; set; }
        public String? PublisDate { get; set; }
        public string? Genre { get; set; }
        public string? Author { get; set; }
    }

}