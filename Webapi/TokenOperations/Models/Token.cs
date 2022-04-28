namespace Webapi.TokenOperations.Models
{
    
    // Connect token endpointi.
    // Bu endpoint geriye acces token, expire token ve refresh token dönecek.

    public class Token{

        public string AccesToken { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }


}