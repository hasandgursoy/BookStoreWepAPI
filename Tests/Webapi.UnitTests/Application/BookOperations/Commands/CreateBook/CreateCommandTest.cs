using System;
using AutoMapper;
using FluentAssertions;
using Webapi.BookOperations.CreateBook;
using Webapi.UnitTests.TestSetup;
using WebApi;
using WebApi.DBOperations;
using Xunit;

namespace Webapi.UnitTests.Application.BookOperations.Commands.CreateCommand
{
    public class CreateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;

        // testFixture da oluşturduğumuz context'i kullanmak için field olarak DBcontext'i çağırıyoruz.
        // daha sonra constructor'da set ediyoruz.
        public CreateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact] // Bir kez çalışan test
        public void WhenAlreadyExistBookTitlesGiven_InvalidOperations_ShouldBeReturn()
        {
            // arrange (Hazırlık)

            var book = new Book()
            {
                Title = "WhenAlreadyExistBookTitlesGiven_InvalidOperations_ShouldBeReturn",
                PageCount = 100,
                PublisDate = new DateTime(1990,01,11),
                GenreId = 1,
                AuthorId = 1,
                
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = new CreateBookModel() {Title = book.Title};


            // act & assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten Mevcut");

        }




    }
}