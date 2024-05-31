using Microsoft.IdentityModel.Tokens;
using RentACarProject.DTO;
using RentACarProject.Repository.KorisnikRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RentACarProject.Helpers
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly IConfiguration configuration;
        private readonly IKorisnikRepository korisnikRepository;

        public AuthenticationHelper(IConfiguration configuration, IKorisnikRepository korisnikRepository)
        {
            this.configuration = configuration;
            this.korisnikRepository = korisnikRepository;
        }
        public bool AuthenticationPrincipal(Principal principal)
        {
            if (korisnikRepository.KorisnikWithCedentialsExists(principal.UserNameK, principal.PasswordK))
            {
                return true;
            }
            return false;
        }

        public string GenerateJwt(Principal principal)
        {
            var user = korisnikRepository.GetKorisnikByUserName(principal.UserNameK);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
               new Claim(ClaimTypes.Role, user.Uloga),
               //new Claim(ClaimTypes.NameIdentifier, user.KorisnikId.ToString()),
                //new Claim("Roles", user.Uloga),
               new Claim("Id", user.KorisnikId.ToString())
            };

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                                             configuration["Jwt:Audience"],
                                             claims,
                                             expires: DateTime.Now.AddMinutes(20),
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
