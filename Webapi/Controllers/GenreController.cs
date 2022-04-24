using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Webapi.Application.BookOperations.Queries.GetIdBook;
using Webapi.Application.GenreOperations.Commands.CreateGenre;
using Webapi.Application.GenreOperations.Commands.DeleteGenre;
using Webapi.Application.GenreOperations.Commands.UpdateGenre;
using Webapi.Application.GenreOperations.Queries.GetGenresQuery;
using Webapi.DBOperations;
using WebApi.DBOperations;

namespace Webapi.Controllers
{
    
    [ApiController]
    [Route("[controller]s")]

    public class GenreController: ControllerBase
    {
        private readonly IBookStoreDBContext _context;
        private readonly IMapper _mapper;

        public GenreController(IBookStoreDBContext context, IMapper mapper)
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
            command.Model = newModel;
            command.GenreId = id;
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();


        }

        [HttpDelete("{id}")]

        public IActionResult DeleteGenre(int id){
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();

        }

    }

}