using Bok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bok.Data.ViewModels
{
    public class NewBookDropdownsVM
    {
        public NewBookDropdownsVM()
        {
           
            Publishers = new List<Publisher>();
            Authors = new List<Author>();
        }

       
        public List<Publisher> Publishers { get; set; }
        public List<Author> Authors { get; set; }
    }
}
