using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Webapi.Common;
using WebApi.DBOperations;

namespace Webapi.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDBContext Context {get;set;}
        public IMapper Mapper {get;set;}
 
        public CommonTestFixture()
        {
            // Normalde Biz DI kullanarak IBookStoreDBContext'den oluşturabiliyoruz ancak burda olmuyor Fake Data Yapamıyoruz interface'lerle birlikte.
            var options = new DbContextOptionsBuilder<BookStoreDBContext>().UseInMemoryDatabase(databaseName:"BookStoreTestDBContext").Options;
            Context = new BookStoreDBContext(options);
            // Database'in yaratıldığından emin ol.
            Context.Database.EnsureCreated();
            Context.AddBooks();
            Context.AddGenres();
            Context.AddAuthors();
            Context.AddUsers();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(config => {config.AddProfile<MappingProfile>();}).CreateMapper();
            


        }
    }
}