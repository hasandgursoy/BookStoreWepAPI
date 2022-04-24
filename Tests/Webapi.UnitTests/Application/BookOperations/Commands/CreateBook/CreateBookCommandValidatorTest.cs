using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Webapi.BookOperations.CreateBook;
using Webapi.UnitTests.TestSetup;
using WebApi.DBOperations;
using Xunit;

namespace Webapi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        public readonly BookStoreDBContext _context;
        public readonly IMapper _mapper;

        [Theory] // Daha az kodla daha fazla test yapabilmek için Theory harika bir olay.
        [InlineData("Lord Of The Rings", 100, 0, 1)]
        [InlineData("Lord Of The Rings", 0, 1, 0)]
        [InlineData("Lord Of The Rings", 100, 0, 1)]
        [InlineData("", 0, 0, 0)]
        [InlineData("", 100, 1, 2)]
        [InlineData("", 0, 1, 1)]
        [InlineData("Lord", 100, 0, 0)]
        [InlineData("Lor", 0, 0, 2)]
        [InlineData("Lord", 0, 1, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnsErros(string title, int pagecount, int genreId, int authorId)
        {
            // Arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                PageCount = pagecount,
                PublishDate = System.DateTime.Now.Date.AddYears(-1),
                GenreId = genreId,
                AuthorId = authorId,
            };
            // act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {

            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 250,
                PublishDate = System.DateTime.Now.Date.AddYears(-1),
                GenreId = 1,
                AuthorId = 1,
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldBeNotReturnError()
        {

            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 250,
                PublishDate = System.DateTime.Now.Date.AddYears(-2),
                GenreId = 1,
                AuthorId = 1,
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
  
        [Fact]

        public void WhenValidInputAreGiven_Book_ShouldBeCreated(){
            
            // Arrange
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            CreateBookModel model = new CreateBookModel(){
                Title = "Hobbit",
                PageCount = 250,
                PublishDate = DateTime.Now.Date.AddYears(-10),
                GenreId = 1,
                AuthorId = 1
            };

            command.Model = model;
            
            // Act (Bu kısımda sadece invoke etmek istiyorsak en sona invoke yazmak lazım.)
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            // Assert
            var book = _context.Books.SingleOrDefault(book => book.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublisDate.Should().Be(model.PublishDate);
            book.Title.Should().Be(model.Title);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.GenreId);



        }
    }   
}