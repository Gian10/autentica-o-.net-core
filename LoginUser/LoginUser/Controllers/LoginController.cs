using LoginUser.Helpers;
using LoginUser.Models;
using LoginUser.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LoginUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private IConfiguration _config;
        private readonly LoginService _loginService;
        private readonly AppSettings _appSettings;

        public LoginController(IConfiguration Configuration, LoginService loginService, IOptions<AppSettings> appSettings)
        {
            _config = Configuration;
            _loginService = loginService;
            _appSettings = appSettings.Value;
        }



        [HttpPost]
        public ActionResult<AuthenticateResponse> Login([FromBody] AuthenticateRequest login)
        {
            var resultado = _loginService.validateUser(login);
            if (resultado != null)
            {
                var token = generateJwtToken(resultado);
                return new AuthenticateResponse(resultado, token);
            }
            else
            {
                return Unauthorized();
            }
        }


        private string generateJwtToken(User user)
        {
            // GERAR TOKEN COM VALIDADE DE 7 DIAS
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
