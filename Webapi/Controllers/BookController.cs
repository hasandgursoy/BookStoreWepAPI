using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Webapi.BookOperations.CreateBook;
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

        public BookController(BookStoreDBContext context)
        {
            _context = context;
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

            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        // Id'ye göre book dönen method
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            GetIdBook vm = new GetIdBook(_context);

            try
            {
                vm.bookID = id;
                var result = vm.Handle();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }





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

            try
            {
                CreateBookCommand command = new CreateBookCommand(_context);
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();
        }


        //// Put ////

        [HttpPut("{id}")]
        public IActionResult UpdatedBook(int id, [FromBody] UpdateBookModels updatedBook)
        {

            try
            {
                UpdateBooks book = new UpdateBooks(_context);
                book.updateBookModels = updatedBook;
                book.ID = id;
                book.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();


        }

        //// Delete ////

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {

            var book = _context.Books.SingleOrDefault(x => x.ID == id);

            if (book is null)
            {
                return BadRequest();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();

        }

    }


}
