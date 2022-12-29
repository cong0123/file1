using Bok.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bok.Models
{
    public class Publisher: IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Publisher Logo")]
        [Required(ErrorMessage = "Publisher logo is required")]
        public string Logo { get; set; }

        [Display(Name = "Publisher Name")]
        [Required(ErrorMessage = "Publisher name is required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Publisher description is required")]
        public string Description { get; set; }

        //Relationships
        public List<Book> Books { get; set; }
    }
}
