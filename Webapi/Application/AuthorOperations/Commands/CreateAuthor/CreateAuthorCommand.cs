using AutoMapper;
using Webapi.Entities;
using WebApi.DBOperations;

namespace Webapi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {

        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandModel Model;
        
        public CreateAuthorCommand(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Name.Trim().ToLower() != Model.Name.Trim().ToLower() && x.SurName.Trim().ToLower() != Model.SurName.Trim().ToLower());
            if (author is null)
            {
                throw new InvalidOperationException("İsim bulunamadı.");
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
    }

}