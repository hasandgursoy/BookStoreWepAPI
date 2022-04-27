using AutoMapper;
using Webapi.Application.AuthorOperations.Commands.CreateAuthor;
using Webapi.Application.AuthorOperations.Queries.GetAuthorDetailQuery;
using Webapi.Application.AuthorOperations.Queries.GetAuthorsQuery;
using Webapi.Application.GenreOperations.Queries.GetGenresQuery;
using Webapi.Application.UserOperations.Commands.CreateUserCommand;
using Webapi.BookOperations.CreateBook;
using Webapi.BookOperations.GetBooks;
using Webapi.BookOperations.GetIdBook;
using Webapi.BookOperations.UpdateBook;
using Webapi.Entities;
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
            CreateMap<Book,GetIdBookModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author, opt=>opt.MapFrom(src => src.Author.Name+" "+src.Author.SurName));;
            CreateMap<Book,BooksViewModel>().ForMember(dest => dest.Genre,opt=> opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author, opt=>opt.MapFrom(src => src.Author.Name+" "+src.Author.SurName));
            CreateMap<UpdateBookModels,Book>();
            
            CreateMap<Genre,GenresViewModel>();
            CreateMap<Genre,GenresDetailViewModel>();

            CreateMap<Author,AuthorsQueryModel>();
            CreateMap<Author,AuthorDetailQueryModel>();
            CreateMap<CreateAuthorCommandModel,Author>();
            
            CreateMap<CreateUserModel,User>();
            
        }

    }

}

// Include olayını anlatıyorum hasancım iyi oku ve tekrar et.
// 1. Book entitisine gidip GenreId'nin hemen altında Genre Genre propu ekliyorsun ve foreighn key ilişkisi kuruyorsun
// 2. İlişki kuruldukdan sonra sorgu yapılan yerdeki db uzantasına örnek _dbcontext.Books include ediyorsun neyi ? Genre'yı çünkü db de o veri yok .
// 3. Mapper da olulturduğun veriye diyorsun ki string Genre'ya Book.Genre.Name ' i ekle.
// 4. En sonunda Enum kullanma sorunu ortadan kalkmış oluyor.