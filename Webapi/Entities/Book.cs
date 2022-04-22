using System;
using System.ComponentModel.DataAnnotations.Schema;
using Webapi.Entities;

namespace WebApi
{

    public class Book
    {
        // Auto İncrement for database 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string? Title { get; set; }

        public int GenreId { get; set; }

        public Genre? Genre {get;set;}

        public int PageCount { get; set; }
        
        public DateTime PublisDate{get;set;}

        
    }

    // GenreId ile Genre arasında şuan foreign key ilişkisi oldu artık.

}