﻿using Bok.Data.Base;
using Bok.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bok.Data.Services
{
    public class AuthorsService : EntityBaseRepository<Author>, IAuthorsService
    {
        public AuthorsService(AppDbContext context) : base(context) { }
    }
}
