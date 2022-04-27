namespace Webapi.Entities
{
    public class User
    {
        public int Id {get;set;}
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? RefreshToken { get; set; } 
        public DateTime? RefreshTokenExpireDate { get; set; }

        // Acces Token uygulamaya giriş yapıldığında tahsis edilir. Life time ve time zonu önemli olan yapı acces tokendir.
        // Acces tokenın 15 dakikalık bir ömrü olduğunu düşünelim. Kullanıcı 15 dakika sonra uygulamadan çıkarılırsa bu büyük bir sorundur.
        // Kullanıcı kendisi çıkmak istediği zaman token'ın süresinin dolması gerekir.
        // Refresh token tam bu noktada devereye giriyor. Sanki Kullanıcı bilgilerini tekrar girip onun yerine giriş yapıyormuş gibi düşünülebilir.
        // RefreshTokenExpireDate ise RefreshToken'ın sona erme süresini yönetmek için içindir.
        // Normalde Token Yönetimi Tahsisi vs. ayrı bir app'de yapılmadılır çünkü çok farklı yapılardır. Farklı DataBase'lerde tutulur ancak biz eğitim amaçlı burada tutuyoruz.
        

    }
}