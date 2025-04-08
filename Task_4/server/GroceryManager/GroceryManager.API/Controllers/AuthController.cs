using GroceryManager.Core.Dtos;
using GroceryManager.Core.Repositories;
using GroceryManager.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroceryManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISupplierRepository _suppliertRepository;
        public AuthController(IConfiguration configuration, ISupplierRepository fridgeRepository)
        {
            _configuration = configuration;
            _suppliertRepository = fridgeRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SupplierLoginDto loginModel)
        {
            try
            {
                if (loginModel == null || string.IsNullOrEmpty(loginModel.CompanyName) || string.IsNullOrEmpty(loginModel.Password))
                {
                    return BadRequest("❌ נתונים חסרים. יש להזין שם משתמש וסיסמה.");
                }

                var supp = await _suppliertRepository.GetByNameAsync(loginModel.CompanyName);

                if (supp == null)
                {
                    return Unauthorized(new { message = "UserNotFound" });
                }

                if (supp.Password != HashPassword(loginModel.Password))
                {
                    return Unauthorized(new { message = "WrongPassword" });
                }
                string suppRole = supp.Role ?? "Supp"; 

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, supp.CompanyName),
            new Claim(ClaimTypes.Role, suppRole), 
            new Claim("suppID", supp.Id.ToString())
        };

                var key = _configuration.GetValue<string>("JWT:Key");
                if (string.IsNullOrEmpty(key))
                {
                    return StatusCode(500, "❌ בעיה בשרת - מפתח JWT לא מוגדר.");
                }
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration.GetValue<string>("JWT:Issuer"),
                    audience: _configuration.GetValue<string>("JWT:Audience"),
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(60), 
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new { Token = tokenString, Role = suppRole, suppID = supp.Id,Name = supp.CompanyName });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ שגיאה בשרת: {ex.Message}");
            }
        }


        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

    }
}
