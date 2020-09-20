using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Mvc;
using TestProject.WebAPI.Models;
using TestProject.WebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("Post")]
        public async Task<ActionResult> CreateUser(UserModel user)
        {
            var email = await userService.Create(user);

            return Created("Created",email);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<UserModel>> GetUser(string email)
        {
            var user = await userService.Get(email);

            return Ok(user);
        }

        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<UserModel>>> ListUsers()
        {
            var users = await userService.List();

            return Ok(users);
        }
    }
}
