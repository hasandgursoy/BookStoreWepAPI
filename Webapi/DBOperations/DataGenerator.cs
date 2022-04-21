using Microsoft.EntityFrameworkCore;
using Webapi.Entities;
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

                context.Genres.AddRange(
                    new Genre{
                        Name = "Personal Growth"

                    },
                    new Genre{
                        Name = "Science Fiction"
                    },
                    new Genre{
                        Name = "Romance"
                    }

                );

                // Context artık bir data base ve static değil
                context.Books.AddRange(
                    new Book
                    {
                        
                        Title = "Herland",
                        GenreId = 1, // Personal Growth
                        PageCount = 200,
                        PublisDate = new DateTime(2001, 06, 12),
                    },
                    new Book
                    {
                        
                        Title = "Lean Startup",
                        GenreId = 2, // Science Fiction
                        PageCount = 250,
                        PublisDate = new DateTime(2010, 05, 23),
                    },
                    new Book
                    {
                        
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