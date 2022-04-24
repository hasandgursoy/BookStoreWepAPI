using System;
using WebApi;
using WebApi.DBOperations;

namespace Webapi.UnitTests.TestSetup
{

    public static class Books
    {
        // BU BİR EXTENSİON METHOD YANİ .ADDRANGE() MESELA :D
        public static void AddBooks(this BookStoreDBContext context)
        {
            context.Books.AddRange(
            new Book
            {
                Title = "Herland",
                GenreId = 1, // Personal Growth
                AuthorId = 1,
                PageCount = 200,
                PublisDate = new DateTime(2001, 06, 12),
            },
            new Book
            {
                Title = "Lean Startup",
                GenreId = 2, // Science Fiction
                AuthorId = 2,
                PageCount = 250,
                PublisDate = new DateTime(2010, 05, 23),
            },
            new Book
            {
                Title = "Dune",
                GenreId = 2, // Science Fiction
                AuthorId = 3,
                PageCount = 540,
                PublisDate = new DateTime(2002, 12, 21),
            });
        }
    }

}