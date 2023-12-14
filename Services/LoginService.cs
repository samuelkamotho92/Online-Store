using Microsoft.IdentityModel.Tokens;
using Online_Store.Models;
using Online_Store.Services.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Online_Store.Services
{
    public class LoginService : IJWT
    {
        //use builder from program
        private readonly IConfiguration _configuration;

        public LoginService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public string GenerateToken(User user)
        {         
            var secretKey = _configuration.GetSection("JWToptions:SecretKey").Value;
            var audence = _configuration.GetSection("JWToptions:Audience").Value;
            var issuer = _configuration.GetSection("JWToptions:Issuer").Value;
            //create key based on secretKey,audience,issuer
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            //set security algorithm
            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            //payload
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("roles",user.roles));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.name.ToString()));
            //token descriptor - expiry dates
            var tokenDesc = new SecurityTokenDescriptor() { 
            Issuer = issuer,
            Audience = audence,
            Expires = DateTime.UtcNow.AddHours(3),
            Subject=new ClaimsIdentity(claims),
            SigningCredentials=cred
            };
            var tk = new JwtSecurityTokenHandler().CreateToken(tokenDesc);
            return new JwtSecurityTokenHandler().WriteToken(tk);
           
        }

    }
}
