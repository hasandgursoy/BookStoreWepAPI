using AutoMapper;
using Webapi.DBOperations;
using Webapi.Entities;

namespace Webapi.Application.UserOperations.Commands.CreateUserCommand
{
    public class CreateUserCommand
    {
        private readonly IBookStoreDBContext _dbContext;
        private readonly IMapper _mapper;
        public CreateUserModel Model { get; set; }

        public CreateUserCommand(IBookStoreDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle(){
            var user = _dbContext.Users.SingleOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);

            if(user is not null){
                throw new InvalidOperationException("Kullanıcı zaten var.");
            }

            user = _mapper.Map<User>(Model);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        
        }   



    }
    public class CreateUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }
        public string Password {get;set;}
    }


}