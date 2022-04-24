using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Webapi.Application.AuthorOperations.Commands.CreateAuthor;
using Webapi.Application.AuthorOperations.Commands.DeleteAuthor;
using Webapi.Application.AuthorOperations.Commands.UpdateAuthor;
using Webapi.Application.AuthorOperations.Queries.GetAuthorDetailQuery;
using Webapi.Application.AuthorOperations.Queries.GetAuthorsQuery;
using Webapi.DBOperations;
using WebApi.DBOperations;

namespace Webapi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class AuthorController:ControllerBase
    {

        private readonly IBookStoreDBContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors(){

            GetAuthorsQuery query = new GetAuthorsQuery(_context,_mapper);
            var obj = query.Handle();

            return Ok(obj);

        }


        [HttpGet("{id}")]
        public IActionResult GetAuthorDetailById(int id){

            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mapper);
            query.AuthorID =id;
            GetAuthorDetailValidator validator = new GetAuthorDetailValidator();
            validator.ValidateAndThrow(query);
            var obj = query.Handle();
            return Ok(obj);
        }

        
        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorCommandModel newModel){

            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            command.Model = newModel;
            CreateAuthorValidator validator = new CreateAuthorValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();

        }

        [HttpPut("{id}")]

        public IActionResult UpdateAuthor(int id,[FromBody] UpdateAuthorCommandModel newModel){

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorID = id;
            command.Model = newModel;
            UpdateAuthorValidator validator = new UpdateAuthorValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();


        }

        [HttpDelete("{id}")]

        public IActionResult DeleteAuthor(int id){
            
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorID = id;
            DeleteAuthorValidator validator = new DeleteAuthorValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();

        }






    }
}