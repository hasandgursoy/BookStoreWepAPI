using System;
using AutoMapper;
using FluentAssertions;
using Webapi.Application.AuthorOperations.Commands.CreateAuthor;
using Webapi.Entities;
using Webapi.UnitTests.TestSetup;
using WebApi.DBOperations;
using Xunit;

namespace Webapi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{

    public class CreateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameAndSurNameGiven_InvalidOperations_ShouldBeReturn()
        {

            // Arrange (Hazırlık)

            var author = new Author()
            {
                Name="hasan",
                SurName ="gursoy"
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            command.Model = new CreateAuthorCommandModel{ Name = author.Name, SurName = author.SurName};
            

            // Acs & Assert
            FluentActions
                .Invoking(() => command.Handle()) // act
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Eklenecek yazar zaten mevcut.");



        }


    }

}