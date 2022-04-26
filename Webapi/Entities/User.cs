namespace Webapi.Entities
{
    public class User
    {
        public int Id {get;set;}
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        //Login dan geçtikden sonra bir tanede refresh token geliyor. Oturum açık kaldığı sürece devam eder.
        // Refresh token tahsis edilir. Yada 15 dk kalma süreside verilebilir örnek :D ama bu saçma tabi ki.
        public string? RefreshToken { get; set; } 
        public DateTime? RefreshTokenExpireDate { get; set; }

    }
}