using Expense_Tracker.ResponseJson;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime;
using System.Security.Claims;

namespace Expense_Tracker.Services
{
    public class TokenService
    {
        public static Token CreateAccesToken(string userId)
        {
            var claims = new List<Claim> { new("UserId", userId)};

            claims.Add(new Claim(ClaimTypes.Role, "User"));
            var jwt = new JwtSecurityToken(issuer: AuthOptions.ISSUER,// создание токенов для возвращения метода
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromHours(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymSecurityKey(), SecurityAlgorithms.HmacSha256));
            claims.Add(new Claim(ClaimTypes.Authentication, new JwtSecurityTokenHandler().WriteToken(jwt)));

            return new Token { AccesToken = new JwtSecurityTokenHandler().WriteToken(jwt) };
        }
    }
}
