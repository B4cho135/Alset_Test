using Core.Entities.Users;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class JwtHelper
    {
        public static string GenerateToken(string secret, UserEntity user, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = System.Text.Encoding.ASCII.GetBytes(secret);

            var claims = new List<Claim>()
            {
                new Claim(type:JwtRegisteredClaimNames.Sub, value: user.UserName),
                new Claim(type:JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                new Claim(type:JwtRegisteredClaimNames.Email, value: user.UserName),
                new Claim(type: "id", user.Id.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(type: ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims: claims),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), algorithm: SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
