using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.DBOperations;

namespace Webapi.DBOperations
{
    public class DataGenerator
    {

        public static void Inıtialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDBContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDBContext>>()))
            {
                //Veri varsa döndür hiç çalıştırma
                if (context.Books.Any())
                {

                    return;
                }
                // Context artık bir data base ve static değil
                context.Books.AddRange(
                    new Book
                    {
                        ID = 1,
                        Title = "Herland",
                        GenreId = 1, // Personal Growth
                        PageCount = 200,
                        PublisDate = new DateTime(2001, 06, 12),
                    },
                    new Book
                    {
                        ID = 2,
                        Title = "Lean Startup",
                        GenreId = 2, // Science Fiction
                        PageCount = 250,
                        PublisDate = new DateTime(2010, 05, 23),
                    },
                    new Book
                    {
                        ID = 3,
                        Title = "Dune",
                        GenreId = 3, // Science Fiction
                        PageCount = 540,
                        PublisDate = new DateTime(2002, 12, 21),
                    }   
                );

                //Books'un altına 3 tane veri ekledim git bunları kaydet diyecez şimdi
                context.SaveChanges();
                
            }
        }
    }
}