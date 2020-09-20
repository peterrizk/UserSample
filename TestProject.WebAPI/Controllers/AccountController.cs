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
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("Post")]
        public async Task<ActionResult> CreateAccounts(AccountModel account)
        {
            await userService.CreateAccount(account);

            return Created("Created", account.Points);
        }

        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<UserModel>>> ListAccounts()
        {
            var users = await userService.ListAccounts();

            return Ok(users);
        }
    }
}
