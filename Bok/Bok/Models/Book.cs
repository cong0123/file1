using Bok.Data;
using Bok.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bok.Models
{
    public class Book:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
       
        public BookCategory BookCategory { get; set; }

        //Relationships
        public List<Author_Book> Authors_Books { get; set; }

        //Publiser
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public Publisher Publishers { get; set; }
        


    }
}
