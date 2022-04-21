using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi
{

    public class Book
    {
        // Auto İncrement for database 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string? Title { get; set; }

        public int GenreId { get; set; }

        public int PageCount { get; set; }
        
        public DateTime PublisDate{get;set;}

        
    }


}