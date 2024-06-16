using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi_JWT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration Configuration;


        public TokenController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpPost(Name = "GetToken")]
        public string Post()
        {

            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "Jose"),
                new Claim("Color", "Naranja"),
                new Claim("Rol", "Admin"),
            };




            var jwtToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(2),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"]!)
                    ),
                    SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);



        }

        [HttpGet(Name = "GetTokensList")]
        public IEnumerable<int> Get()
        {
            return [7, 9, 0];
        }
    }
}
