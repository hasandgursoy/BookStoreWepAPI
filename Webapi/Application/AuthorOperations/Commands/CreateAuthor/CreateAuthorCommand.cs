using AutoMapper;
using Webapi.DBOperations;
using Webapi.Entities;
using WebApi.DBOperations;

namespace Webapi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {

        private readonly IBookStoreDBContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandModel Model{get;set;}
        
        public CreateAuthorCommand(IBookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Name == Model.Name &&  x.SurName == Model.SurName);
            if (author is not null)
            {
                throw new InvalidOperationException("Eklenecek yazar zaten mevcut.");
            }

            Author query = _mapper.Map<Author>(Model);
            _context.Authors.Add(query);
            _context.SaveChanges();
        }
    }

    public class CreateAuthorCommandModel
    {
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsBookPublished {get;set;} = true;
    }

}