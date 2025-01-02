using authentication_authorization.Data;
using authentication_authorization.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace authentication_authorization.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("registration")]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(LoginDTO loginDTO)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == loginDTO.Email && u.Password == loginDTO.Password);
            if (user == null)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("GetUsers")]
        public ActionResult GetUsers()
        {
            return View();
        }
=
        [HttpPost]
        [Route("GetUser")]
        public ActionResult GetUser(int id)
        {
            return View();
        }

        
    }
}
