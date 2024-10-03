using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PracticeProject.Repositories.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PracticeProject.Repositories.Implementation
{
    public class TokenRepo : ITokenRepo
    {
        private readonly IConfiguration configuration;

        public TokenRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            //Create Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)

                //new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                //new Claim(ClaimTypes.Name, user.UserName ?? string.Empty)              //*create claim for user's name
            };

           // claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Name, role))); //iterating each of roles and converting that to a claim

            //*Add the user's roles to claim
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            //Signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Create the token
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            //Return Token
            return new JwtSecurityTokenHandler().WriteToken(token); 
        }

    }
}
