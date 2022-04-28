using System;
using AutoMapper;
using FluentAssertions;
using Webapi.Application.AuthorOperations.Commands.CreateAuthor;
using Webapi.UnitTests.TestSetup;
using WebApi.DBOperations;
using Xunit;

namespace Webapi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorValidatorTest : IClassFixture<CommonTestFixture>
    {

        public readonly BookStoreDBContext _context;
        public readonly IMapper _mapper;

        public CreateAuthorValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData("a", "")]
        [InlineData("a", "gursoy")]
        [InlineData("Hasan", "s")]
        [InlineData("a", "b")]
        public void WhenInvalidInputsAreGiven_Validator_ShoulBeReturnErrors(string name, string surname)
        {
            // Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorCommandModel()
            {
                Name = name,
                SurName = surname,
                DateOfBirth = DateTime.Now.Date.AddYears(-1),

            };

            // Act
            CreateAuthorValidator validator = new CreateAuthorValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }


    }
}