using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi;
using WebApi.DBOperations;

namespace Webapi.Controllers
{
    
    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {
        private readonly BookStoreDBContext _context;

        public BookController(BookStoreDBContext context){
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
        public List<Book> GetBooks(){

            List<Book> bookList = BookList.OrderBy(x => x.ID).ToList<Book>();
            return bookList;
        }

        // Id'ye göre book dönen method
        [HttpGet("{id}")]
        public Book GetById(int id){

            var book = BookList.Where(x => x.ID == id).SingleOrDefault();
            return book == null? BookList[0] : book ;
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
        public IActionResult AddBook([FromBody] Book newBook){

            var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
            if(book is not null){
                return BadRequest();
            }

            BookList.Add(newBook);
            return Ok();


        }


        //// Put ////

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] Book updatedBook){

            var book = BookList.SingleOrDefault(x => x.ID == id);

            if(book is null){
                return BadRequest();
            }

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublisDate = updatedBook.PublisDate != default ? updatedBook.PublisDate : book.PublisDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            return Ok();



        }

        //// Delete ////

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id){

            var book = BookList.SingleOrDefault(x => x.ID == id);

            if(book is null){
                return BadRequest();
            }

            BookList.Remove(book);
            return Ok();

        }

    }


}
