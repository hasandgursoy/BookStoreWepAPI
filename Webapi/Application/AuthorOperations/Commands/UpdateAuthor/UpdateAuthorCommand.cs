using WebApi.DBOperations;

namespace Webapi.Application.AuthorOperations.Commands.UpdateAuthor
{

    public class UpdateAuthorCommand
    {
        public int AuthorID { get; set; }
        private readonly BookStoreDBContext _context;
        public UpdateAuthorCommandModel Model { get; set; }
        public UpdateAuthorCommand(BookStoreDBContext context)
        {
            _context = context;
        }

        public void Handle()
        {

            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorID);
            if (author is null)
            {
                throw new InvalidOperationException("Yazar Bulunamadı");
            }

            author.Name = string.IsNullOrEmpty(Model.Name.Trim()) || author.Name == Model.Name ? author.Name : Model.Name;
            author.SurName = string.IsNullOrEmpty(Model.SurName.Trim()) || author.SurName == Model.SurName ? author.SurName : Model.SurName;
            author.DateOfBirth = string.IsNullOrEmpty(Model.DateOfBirth.ToString()) || author.DateOfBirth == author.DateOfBirth ? author.DateOfBirth : Model.DateOfBirth;
            

            _context.SaveChanges();

        }
    }

    public class UpdateAuthorCommandModel
    {
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}