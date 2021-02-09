using LoginUser.Data;
using LoginUser.Functions;
using LoginUser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LoginUser.Services
{
    public class LoginService
    {
        private readonly LoginUserContext _context;

        public LoginService(LoginUserContext loginUserContext)
        {
            _context = loginUserContext;
        }

        EncriptografarDescriptografar md5 = new EncriptografarDescriptografar();

        public User validateUser(AuthenticateRequest tokenLogin)
        {
            try
            {
               // var EncriptPassword = md5.Encrypt(tokenLogin.Password);
                var res = _context.user.Where(u => u.Name == tokenLogin.Name & u.Password == tokenLogin.Password).FirstOrDefault<User>();
                return res;
            }
            catch(InvalidCastException e)
            {   
                return null;
            }
        }
    }
}
