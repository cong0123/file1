using Bok.Data.Base;
using Bok.Data.ViewModels;
using Bok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bok.Data.Services
{
    public interface IBooksService:IEntityBaseRepository<Book>
    {
        Task<Book> GetBookByIdAsync(int id);
        Task<NewBookDropdownsVM> GetNewBookDropdownsValues();
        Task AddNewBookAsync(NewBookVM data);
        Task UpdateBookAsync(NewBookVM data);



    }
}
