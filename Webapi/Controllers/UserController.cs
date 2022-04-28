using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Webapi.Application.UserOperations.Commands.CreateToken;
using Webapi.Application.UserOperations.Commands.CreateUserCommand;
using Webapi.Application.UserOperations.Commands.RefreshToken;
using Webapi.DBOperations;
using Webapi.TokenOperations.Models;

namespace Webapi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {

        private readonly IBookStoreDBContext _dbContext;
        private readonly IMapper _mapper;
        // IConfiguration appsettings.json dosyasındaki verilere ulaşmamızı sağlıyor.
        private IConfiguration _configuration;


        public UserController(IBookStoreDBContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel model)
        {

            CreateUserCommand command = new CreateUserCommand(_dbContext,_mapper);
            command.Model = model;
            CreateUserValidator valdiator = new CreateUserValidator();
            valdiator.Validate(command);
            command.Handle();
            return Ok();

        }

        [HttpPost("connect/token")]

        public ActionResult<Token> Createtoken([FromBody] CreateTokenModel model){

            CreateTokenCommand command = new CreateTokenCommand(_configuration,_mapper,_dbContext);
            command.Model = model;
            CreateTokenValidator validator = new CreateTokenValidator();
            validator.Validate(command);
            var token = command.Handle();
            return token;

        }

        [HttpGet("refreshtoken")]

        public ActionResult<Token> RefreshToken([FromQuery] string token){

            RefreshTokenCommand command = new RefreshTokenCommand(_dbContext,_configuration);
            command.RefreshToken = token;
            RefreshTokenValidator valdiator = new RefreshTokenValidator();
            valdiator.Validate(command);
            var resultToken = command.Handle();
            return resultToken;
        }

    }

}