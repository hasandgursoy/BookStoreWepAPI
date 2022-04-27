using Webapi.DBOperations;
using Webapi.TokenOperations;
using Webapi.TokenOperations.Models;

namespace Webapi.Application.UserOperations.Commands.RefreshToken
{


    public class RefreshTokenCommand
    {

        public string RefreshToken { get; set; }
        private readonly IBookStoreDBContext _context;
        private readonly IConfiguration _configuration;


        public RefreshTokenCommand(IBookStoreDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Token Handle()
        {

            var user = _context.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
            if (user is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccesToken();

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _context.SaveChanges();
                
                return token;
                
            }
            else
            {
                throw new InvalidOperationException("Valid bir refresh Token bulunamadı.");
            }



        }

    }

}