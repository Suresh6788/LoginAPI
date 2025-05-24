using LoginApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly LoginDbContect _loginDbContect;

        public UserController(LoginDbContect loginDbContect)
        {
            _loginDbContect = loginDbContect;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers() 
        {
            var allUsers =await _loginDbContect.Users.ToListAsync();

            return Ok(allUsers);
        }

        


    }
}
