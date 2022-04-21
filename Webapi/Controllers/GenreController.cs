using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Webapi.Application.BookOperations.Queries.GetIdBook;
using Webapi.Application.GenreOperations.Commands.CreateGenre;
using Webapi.Application.GenreOperations.Commands.UpdateGenre;
using Webapi.Application.GenreOperations.Queries.GetGenresQuery;
using WebApi.DBOperations;

namespace Webapi.Controllers
{
    
    [ApiController]
    [Route("[controller]s")]

    public class GenreController: ControllerBase
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;

        public GenreController(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres(){

            GetGenresQuery query = new GetGenresQuery(_context,_mapper);
            var obj = query.Handle();
            return Ok(obj);

        }

        [HttpGet("{id}")]

        public IActionResult GetGenreDetail(int id){

            GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
            query.GenreId = id;
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpPost]
        
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = newGenre;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]

        public IActionResult UpdateGenre(int id,[FromBody] UpdateGenreCommandModel newModel){
            
            UpdateGenreCommand command = new UpdateGenreCommand(_context);

        }

    }

}