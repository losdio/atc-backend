using atc_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace atc_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        [HttpPost]
        [Route("Registration")]
        public IActionResult Registration(UserDTO userDTO) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var objUser = _context.Users.FirstOrDefault(x => x.Email == userDTO.Email);
            if (objUser == null)
            {
                _context.Users.Add(new Models.User
                {
                    UserName = userDTO.UserName,
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Email = userDTO.Email,
                    Password = userDTO.Password,
                });
                _context.SaveChanges();
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest("User already exists with the same email address.");
            }
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDTO loginDTO) 
        { 
            var user = _context.Users.FirstOrDefault(x => x.UserName == loginDTO.UserName && x.Password == loginDTO.Password);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NoContent();
            }
        }
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers() 
        { 
            return Ok(_context.Users.ToList());
        }
        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);
            if (user != null) 
                return Ok(user);
            else
                return NoContent();
        }
    }

}
