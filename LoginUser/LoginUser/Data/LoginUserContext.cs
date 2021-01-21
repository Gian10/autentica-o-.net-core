using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LoginUser.Models;

namespace LoginUser.Data
{
    public class LoginUserContext : DbContext
    {
        public LoginUserContext (DbContextOptions<LoginUserContext> options)
            : base(options)
        {
        }

        public DbSet<LoginUser.Models.User> user { get; set; }
    }
}
