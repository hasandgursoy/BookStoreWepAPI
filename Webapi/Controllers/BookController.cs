using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Webapi.BookOperations.CreateBook;
using Webapi.BookOperations.DeleteBook;
using Webapi.BookOperations.GetBooks;
using Webapi.BookOperations.GetIdBook;
using Webapi.BookOperations.UpdateBook;
using WebApi;
using WebApi.DBOperations;

namespace Webapi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // private static List<Book> BookList = new List<Book>(){

        //     new Book{
        //         ID  = 1,
        //         Title = "Herland",
        //         GenreId = 1, // Personal Growth
        //         PageCount = 200,
        //         PublisDate = new DateTime(2001,06,12),
        //     },
        //     new Book{
        //         ID  = 2,
        //         Title = "Lean Startup",
        //         GenreId = 2, // Science Fiction
        //         PageCount = 250,
        //         PublisDate = new DateTime(2010,05,23),
        //     },
        //     new Book{
        //         ID  = 3,
        //         Title = "Dune",
        //         GenreId = 3, // Science Fiction
        //         PageCount = 540,
        //         PublisDate = new DateTime(2002,12,21),
        //     },

        // };


        // Http Requestlerini karşılayan endpointleri yazalım.
        // Bütün book listesini dönen method (endpoint)
        [HttpGet]
        public IActionResult GetBooks()
        {

            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        // Id'ye göre book dönen method
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            GetIdBook vm = new GetIdBook(_context, _mapper);
            vm.bookID = id;

            
                GetIdBookValidator validator = new GetIdBookValidator();
                validator.ValidateAndThrowAsync(vm);
                var result = vm.Handle();
                return Ok(result);
           





        }

        // Sadece Bir tane parametresiz httpget kullanılabilir.
        // Normal HttpGet daha mantıklı bir yol
        // [HttpGet]
        // public Book Get([FromQuery] string id){

        //     var book = BookList.Where(book => book.ID == Convert.ToInt32(id)).SingleOrDefault();
        //     return book == null? BookList[0] : book ;
        // }

        //// Post ////
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {

            
                CreateBookCommand command = new CreateBookCommand(_context, _mapper);
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                // Hata'yı geri dön ekleme yada değiştirme demek için ValidateAndthrow.
                // Hata alınırsa artık bunu Oluşturduğumuz CustomMiddleWare'de yakalanıcak.
                validator.ValidateAndThrow(command);
                command.Handle();

                //ValidationResult result = validator.Validate(command);
                // if (!result.IsValid)
                //     foreach (var item in result.Errors)
                //         System.Console.WriteLine("Özellik " + item.PropertyName + " - Error Message: " + item.ErrorMessage);
                // else
                //     command.Handle();

            
            return Ok();
        }


        //// Put ////

        [HttpPut("{id}")]
        public IActionResult UpdatedBook(int id, [FromBody] UpdateBookModels updatedBook)
        {

            
                UpdateBooks book = new UpdateBooks(_context,_mapper,updatedBook);
                book.updateBookModels = updatedBook;
                book.ID = id;
                UpdateBookValidator validator = new UpdateBookValidator();
                validator.ValidateAndThrow(book);
                book.Handle();
                return Ok();
            
            


        }

        //// Delete ////

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {

           
                DeleteBookCommand dellBook = new DeleteBookCommand(_context, id);
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(dellBook);
                dellBook.Handle();
                return Ok();
           





        }

    }


}
