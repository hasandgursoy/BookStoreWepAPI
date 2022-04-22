using System.ComponentModel.DataAnnotations.Schema;

namespace Webapi.Entities
{
    public class Author
    {   
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsBookPublished {get;set;} = false;
    }
}