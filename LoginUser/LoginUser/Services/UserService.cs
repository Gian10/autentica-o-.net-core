using LoginUser.Data;
using LoginUser.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginUser.Services
{
    public class UserService
    {

        private readonly LoginUserContext _context;

        public UserService(LoginUserContext context)
        {
            _context = context;
        }

        public async Task<List<User>> ListUser()
        {
            return await _context.user.ToListAsync();
        }

        public async Task<List<User>> InsertUser(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return await _context.user.ToListAsync();
        }


    }
}
