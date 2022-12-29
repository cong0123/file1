using Bok.Data.Base;
using Bok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bok.Data.Services
{
    public class PublishersService : EntityBaseRepository<Publisher>, IPublishersService
    {
        public PublishersService(AppDbContext context) : base(context)
        {
        }
    }
}