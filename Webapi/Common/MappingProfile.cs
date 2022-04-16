using AutoMapper;
using Webapi.BookOperations.CreateBook;
using Webapi.BookOperations.GetBooks;
using Webapi.BookOperations.GetIdBook;
using Webapi.BookOperations.UpdateBook;
using WebApi;

namespace Webapi.Common
{

    public class MappingProfile : Profile
    {
        
        public MappingProfile()
        {
            // Source - Target
            CreateMap<CreateBookModel, Book>();
            // ForMember() => her satır geldiğinde 
            CreateMap<Book, GetIdBookModel>().ForMember(dest => dest.GenreId, opt => opt.MapFrom(src =>((GenreEnum) src.GenreId).ToString()));
            CreateMap<Book,BooksViewModel>().ForMember(dest => dest.Genre,opt=> opt.MapFrom(src => ((GenreEnum) src.GenreId).ToString()));
            CreateMap<UpdateBookModels,Book>();
        }

    }

}