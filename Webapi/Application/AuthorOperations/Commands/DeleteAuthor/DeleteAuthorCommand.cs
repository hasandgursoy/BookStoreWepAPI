using Webapi.Entities;
using WebApi.DBOperations;

namespace Webapi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly BookStoreDBContext _context;
        public int AuthorID { get; set; }


        public DeleteAuthorCommand(BookStoreDBContext context)
        {
            _context = context;
        }

        public void Handle(){

            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorID);
            if(author is null){
                throw new InvalidOperationException("Yazar bulunamadı.");
            }

             if(author.IsBookPublished is true){
                 throw new InvalidOperationException("Yazarın yayında olan bir kitabı var ! Bu yüzden silinemez !");
             }

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}