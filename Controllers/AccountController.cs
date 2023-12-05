using api.Data;
using api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("register-guest")]
        public async Task<ActionResult<User>> Register([FromQuery] string nick)
        {
            if (string.IsNullOrEmpty(nick)) return BadRequest("Nickname is not included in query parameters");
            if (await UserExists(nick)) return BadRequest("User with this nickname already exists");

            User user = new User
            {
                Nickname = nick,
                IsGuest = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private async Task<bool> UserExists(string nickname)
        {
            return await _context.Users.AnyAsync(x => x.Nickname.ToLower() == nickname.ToLower());
        }
    }
}