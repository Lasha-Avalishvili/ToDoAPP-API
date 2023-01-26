using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoApp_API.Auth;

namespace ToDoApp_API.Auth
{
    public class TokenGenerator
    {
        private readonly JwtSettings _settings;

        public TokenGenerator(JwtSettings settings)
        {
            _settings = settings;
        }
        public string Generate(string email)
        {
            var claims = new List<Claim>
{
    new Claim(JwtRegisteredClaimNames.Sub, email),
    new Claim(ClaimTypes.Role, "api-user"),
};

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecrectKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);

            var tokenGenerator = new JwtSecurityTokenHandler();
            var jwtString = tokenGenerator.WriteToken(token);
            return jwtString;
        }




    }
}
