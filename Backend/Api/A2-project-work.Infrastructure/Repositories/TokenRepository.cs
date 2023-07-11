using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using A2_project_work.ApplicationCore.Abstracts.Repositories;
using A2_project_work.ApplicationCore.Interfaces.Repositories;
using A2_project_work.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace A2_project_work.Infrastructure.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;

        public TokenRepository(IConfiguration conf)
        {
            _configuration = conf;
        }

        public Token GetToken(string username, Guid userID, bool isadmin = false, bool israsp=false)
        {
            DateTime expirationDate = DateTime.UtcNow.AddMinutes(60);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            List<Claim> baseclaimarr = new List<Claim> {
                        new Claim(ClaimTypes.Sid, userID.ToString()), // id utente
                        new Claim(ClaimTypes.Name, username),         // username
              };
            if (isadmin)
            {
                expirationDate = DateTime.UtcNow.AddHours(2);
                baseclaimarr.RemoveAt(baseclaimarr.Count - 1);
                baseclaimarr.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }
            else
            {
                if (israsp)
                {
                    expirationDate = DateTime.UtcNow.AddYears(1);
                    baseclaimarr.RemoveAt(baseclaimarr.Count - 1);
                    baseclaimarr.Add(new Claim(ClaimTypes.Role, "Raspberry"));
                }
                else
                {
                    baseclaimarr.Add(new Claim(ClaimTypes.Role, "User"));
                }
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(baseclaimarr),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Token { token = tokenHandler.WriteToken(token) };
        }

    }
}
