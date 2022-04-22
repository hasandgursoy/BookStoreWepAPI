using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Webapi.Common;
using WebApi.DBOperations;

namespace Webapi.BookOperations.GetIdBook
{

    public class GetIdBook
    {
        public int bookID { get; set; }
        private readonly BookStoreDBContext _dbContext;
        private readonly IMapper _mapper;

        public GetIdBook(BookStoreDBContext dBContext, IMapper mapper)
        {
            this._dbContext = dBContext;
            _mapper = mapper;
        }

        public GetIdBookModel Handle(){

            var book = _dbContext.Books.Include(x => x.Genre).Where(x => x.ID == bookID).SingleOrDefault();
            
            if (book is null)
            {
                throw new InvalidOperationException("Olamayan Değer Girdiniz !");
            }
            
            GetIdBookModel vm = _mapper.Map<GetIdBookModel>(book);

            // vm.Title = book.Title;
            // vm.GenreId = ((GenreEnum)book.GenreId).ToString();
            // vm.PageCount = book.PageCount;
            // vm.PublisDate = book.PublisDate.Date.ToString("dd/MM/yyyy");
            return vm;



        }


    }

    public class GetIdBookModel
    {
        
        public string? Title { get; set; }
        public int PageCount { get; set; }
        public string? PublisDate { get; set; }
        public string? Genre { get; set; }
    }

}