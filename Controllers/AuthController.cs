using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using My_Port.Data;
using My_Port.Dto;
using My_Port.Models;
using System.Security.Claims;
using System.Text;

namespace My_Port.Controllers
{
    public class AuthController(ApplicationDBContext _context) : Controller
    {
        public string UserRole=string.Empty;


        public IActionResult Index()
        {
            var list = _context.Users.Select(x => new UserDto
            {
                UserName = x.UserName,
                Email = x.Email
            }).ToList();

            // var list = _context.UserDtos
            //.FromSqlRaw("EXEC sp_GetUsers")
            //.ToList();


            return View(list);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult LoginToRegister()
        {
            return RedirectToAction("Registration");
        }

        public IActionResult RegisterToLogin()
        {
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> RegisterUser(UserDto dto)
        {
     

            if (dto == null)
            {
                TempData["Info"] = "Please provide email and password.";
                return View("Registration");
            }
            if (dto.Email == null || dto.Password == null)
            {
                TempData["Info"] = "Email and password are required.";
                return View("Registration");
            }

            var data = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (data != null)
            {
                TempData["Info"] = "Email already exists.";
                return View("Registration");
            }

            _context.Users.Add(new User
            {
                UserName = dto.UserName,
                Password = dto.Password,
                Email = dto.Email
            });

            await _context.SaveChangesAsync();
            TempData["Success"] = "Registration successful! Please log in.";
            return RedirectToAction("Login");

        }

        public async Task<IActionResult> LoginUser(UserDto dto)
        {

            var data = await (
                 from u in _context.Users
                 join ur in _context.UserRoles on u.UserId equals ur.UserId
                 join r in _context.Roles on ur.RoleId equals r.RoleId
                 where u.Email.Contains(dto.Email)

                 select new UserDto
                 {
                     UserId = u.UserId,
                     UserName = u.UserName,
                     Email = u.Email,
                     Password = u.Password,
                     UserRole = r.RoleName
                 }
             ).ToListAsync();

            UserRole = data[0].UserRole;

            if (dto == null)
            {
                TempData["Info"] = "Please provide email and password.";
                return View("Login");
            }
            if (dto.Email == null || dto.Password == null)
            {
                TempData["Info"] = "Email and password are required.";
                return View("Login");
            }

            var isExist = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (isExist == null)
            {
                TempData["Info"] = "Email does not exist.";
                return View("Login");
            }

            if (isExist.Password != dto.Password)
            {
                TempData["Info"] = "Incorrect password.";
                return View("Login");
            }

            var token = GenerateJwtToken(dto);

            Response.Cookies.Append("jwt_Token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });
            
            TempData["Success"] = "Login successful!";
            return RedirectToAction("Index", "Home");

        }

        private string GenerateJwtToken(UserDto dto)
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("d2f1d58034bf9137e6e399385843aa23d7fa70970794d0db4f09ff19d249ae5b4b890341");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, dto.Email),
                    new Claim(ClaimTypes.Role, UserRole)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt_Token");
            return RedirectToAction("Login");
        }
    }
}
