using LoginUser.Data;
using LoginUser.Functions;
using LoginUser.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        EncriptografarDescriptografar md5 = new EncriptografarDescriptografar();

        public async Task<List<User>> ListUser()
        {
            return await _context.user.ToListAsync();
        }

        public async Task<List<User>> InsertUser(User user)
        {
            //user.Password = md5.Encrypt(user.Password);
            _context.Add(user);
            await _context.SaveChangesAsync();
            return await _context.user.ToListAsync();
        }


        public User GetById(int id)
        {
            return _context.user.FirstOrDefault(x => x.UserId == id);
        }



    }
}
