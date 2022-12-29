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
    public class NewBookVM
    {
        public int Id { get; set; }

        [Display(Name = "Book name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Book description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Book poster URL")]
        [Required(ErrorMessage = "Book poster URL is required")]
        public string ImageURL { get; set; }

      

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Book category is required")]
        public BookCategory BookCategory { get; set; }

        //Relationships
        [Display(Name = "Select author(s)")]
        [Required(ErrorMessage = "Book author(s) is required")]
        public List<int> AuthorIds { get; set; }

        [Display(Name = "Select a Publisher")]
        [Required(ErrorMessage = "Book publisher is required")]
        public int PublisherId { get; set; }

    }
}
