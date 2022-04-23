using AutoMapper;
using WebApi;
using WebApi.DBOperations;

namespace Webapi.BookOperations.CreateBook
{

    public class CreateBookCommand
    {
        public CreateBookModel Model{get;set;}
        private readonly BookStoreDBContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public void Handle(){

            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

            if (book is not null)
            {
                throw new InvalidOperationException("Kitap zaten Mevcut");
            }
            
            book = new Book();
            book = _mapper.Map<Book>(Model);
            // book.Title = Model.Title;
            // book.PublisDate = Model.PublishDate;
            // book.PageCount = Model.PageCount;
            // book.GenreId = Model.GenreId;
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();



        }

        

    }

    public class CreateBookModel
        {
            public string? Title {get;set;}
            public int GenreId { get; set; }
            public int AuthorId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate{get;set;}

        }

}