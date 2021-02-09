using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoginUser.Data;
using LoginUser.Models;
using LoginUser.Services;
using Microsoft.AspNetCore.Authorization;

namespace LoginUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }

        [Authorize]
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUser()
        {
            var res = await _service.ListUser();
            return Ok(res);
        }

       
        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var res = await _service.InsertUser(user);
            return CreatedAtAction("GetUser", res);
        }
    }
}