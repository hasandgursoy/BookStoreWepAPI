using Webapi.Entities;
using WebApi.DBOperations;

namespace Webapi.UnitTests.TestSetup
{
    public static class Users
    {
        // BU BİR EXTENSİON METHOD YANİ .ADDRANGE() MESELA :D
        public static void AddUsers(this BookStoreDBContext context)
        {
            context.Users.AddRange(
            
            new User
            {
                Name = "Hasan",
                Surname = "Gursoy", // Science Fiction
                Email = "hasan.gursoy@gmail.com",
                Password = "123456789",
                Id = 1,
            });
        }
    }
}