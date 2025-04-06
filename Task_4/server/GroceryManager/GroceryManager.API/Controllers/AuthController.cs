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
        // GET: api/<AuthController>
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
                // 1️⃣ בדיקה אם הנתונים חסרים
                if (loginModel == null || string.IsNullOrEmpty(loginModel.CompanyName) || string.IsNullOrEmpty(loginModel.Password))
                {
                    return BadRequest("❌ נתונים חסרים. יש להזין שם משתמש וסיסמה.");
                }

                // 2️⃣ חיפוש המשתמש במערכת
                var supp = await _suppliertRepository.GetByNameAsync(loginModel.CompanyName);

                if (supp == null)
                {
                    return Unauthorized("⚠️ שם המשתמש לא קיים במערכת.");
                }

                // 3️⃣ בדיקת סיסמה
                if (supp.Password != HashPassword(loginModel.Password))
                {
                    return Unauthorized("🔑 סיסמה שגויה.");
                }
                // 4️⃣ קביעת התפקיד - נשלוף את ה-Role מהמשתמש עצמו
                string suppRole = supp.Role ?? "Supp"; // אם אין תפקיד שמור, ברירת מחדל היא "supp"

                // 5️⃣ יצירת תביעות (`Claims`) לפי תפקיד
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, supp.CompanyName),
            new Claim(ClaimTypes.Role, suppRole), // התפקיד מגיע מהדאטהבייס
            new Claim("suppID", supp.Id.ToString())
        };

                // 6️⃣ יצירת טוקן JWT
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
                    expires: DateTime.UtcNow.AddMinutes(60), // תוקף ארוך יותר
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                // 7️⃣ החזרת הטוקן ללקוח יחד עם תפקיד המשתמש
                return Ok(new { Token = tokenString, Role = suppRole, suppID = supp.Id });
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
