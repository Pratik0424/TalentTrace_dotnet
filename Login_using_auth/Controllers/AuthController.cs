using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Login_using_auth.Data;
using Login_using_auth.Models;

//using Login_using_auth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static Login_using_auth.Models.UserDTO;

namespace Login_using_auth.Controllers
{

    //[Authorize]  // entire controller level
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
            private readonly ApplicationDbContext _context;
            private readonly IConfiguration _configuration;

            public AuthController(ApplicationDbContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            [HttpPost]
            [Route("registration")]
        public IActionResult Registration(User newUser)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == newUser.Email);
            if (existingUser != null)
            {
                return Conflict("User already exists");
            }

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost]
            [Route("login")]
        public IActionResult Login(User loginDTO)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == loginDTO.Email && u.Password == loginDTO.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Sub , _configuration["Jwt:subject"]),
                            new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString() ),
                            new Claim("UserId", user.Id.ToString()),
                            new Claim("Email", user.Email),

                        };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signIn
                );

                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { token = tokenString, user });

            }
            return Unauthorized();
        }

        [HttpPost]
            [Route("GetUsers")]
            public IActionResult GetUsers()
            {
                var users = _context.Users.ToList();
                return Ok(users);
            }

            //[Authorize] // api level or method level
            [HttpPost]
            [Route("GetUser")]
            public IActionResult GetUser([FromBody] GetUserRequest request)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == request.Id);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                return Ok(user);
            }
    }
}
