using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Webapi.Entities;
using Webapi.TokenOperations.Models;

namespace Webapi.TokenOperations
{

    public class TokenHandler
    {

        public IConfiguration Configuration { get; set; }

        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        // User'a göre token dönen bir method yazalım.
        public Token CreateAccesToken()
        {

            Token tokenModel = new Token();

            // Kimlik bilgilerini imzalama
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token'ın sona erme süresi
            tokenModel.Expiration = DateTime.Now.AddMinutes(15);

            JwtSecurityToken securityToken = new JwtSecurityToken(

                issuer:Configuration["Token:Issuer"],
                audience:Configuration["Token:Audience"],
                expires:tokenModel.Expiration,
                // Token üretildikden ne kadar sonra devereye gireceğini söylüyoruz.
                notBefore:DateTime.Now,
                signingCredentials :signingCredentials 

            );
            
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // Acces token artık üretildi yukarıdaki jwtsechandler acces tokeni üretmek için kullandık.
            tokenModel.AccesToken = tokenHandler.WriteToken(securityToken);
            // Refresh Tokenımızda yazalım son aşama return edicez.
            tokenModel.RefreshToken = CreateRefreshToken();
            return tokenModel;

        }

        // Refresh token dönen methodumuz.
        public string CreateRefreshToken(){
            return Guid.NewGuid().ToString();
        }

    }

}