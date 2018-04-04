using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using burger_shack.Models;
using burger_shack.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace burger_shack.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserRepository _repo;

        public AccountController(UserRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<UserReturnModel> Register([FromBody] UserCreateModel userData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserReturnModel user = _repo.Register(userData);
                    ClaimsPrincipal principal = user.SetClaims();
                    await HttpContext.SignInAsync(principal);
                    return user;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return null;
        }

        [HttpPost("login")]
        public async Task<UserReturnModel> Login([FromBody] UserLoginModel userData)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            try
            {
                UserReturnModel user = _repo.Login(userData);
                var principal = user.SetClaims();
                await HttpContext.SignInAsync(principal);
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        [HttpDelete("logout")]
        public async Task<string> Logout()
        {
            await HttpContext.SignOutAsync();
            return "successfully logged out";
        }

        [Authorize]
        [HttpPut]
        public UserReturnModel UpdateAccount([FromBody] UserReturnModel userData)
        {
            var id = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Name)
                .Select(c => c.Value).SingleOrDefault();
            UserReturnModel user = _repo.GetUserById(id);
            return _repo.UpdateAccount(user, userData);
        }

        [HttpGet("authenticate")]
        public UserReturnModel Authenticate()
        {
            var user = HttpContext.User;
            var id = user.Identity.Name;
            if (id == null)
            {
                return null;
            }
            else
            {
                return _repo.GetUserById(id);
            }
        }
    }
}