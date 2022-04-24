using Microsoft.EntityFrameworkCore;
using Webapi.Entities;
using WebApi;

namespace Webapi.DBOperations
{
    public interface IBookStoreDBContext
    {

        public DbSet<Book> Books {get;set;}
        public DbSet<Genre> Genres {get;set;}
        public DbSet<Author> Authors {get;set;}

        int SaveChanges();

    }
}