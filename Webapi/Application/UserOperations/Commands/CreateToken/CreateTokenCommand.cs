using AutoMapper;
using Webapi.DBOperations;
using Microsoft.Extensions.Configuration;
using Webapi.TokenOperations;
using Webapi.TokenOperations.Models;

namespace Webapi.Application.UserOperations.Commands.CreateToken
{


    public class CreateTokenCommand
    {
        private readonly IBookStoreDBContext _dbContext;
        private readonly IMapper _mapper;
        public CreateTokenModel Model { get; set; }
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IConfiguration configuration, IMapper mapper, IBookStoreDBContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Token Handle()
        {

            var user = _dbContext.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if (user is not null)
            {

                // Create a Token
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccesToken(user);
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _dbContext.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("Kullanici Adi veya Şifre Hatalı");
            }

        }


    }

    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }



}