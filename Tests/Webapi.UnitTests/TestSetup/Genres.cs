using Webapi.Entities;
using WebApi.DBOperations;

namespace Webapi.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDBContext context)
        {

            context.Genres.AddRange(
                    new Genre
                    {Name = "Personal Growth"},
                    new Genre
                    {Name = "Science Fiction"},
                    new Genre
                    {Name = "Romance"});


        }
    }
}