using _1_Pagination.Models;
using _1_Pagination.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _1_Pagination.TokenServices
{
    public class MyTokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private AppUser? _user;

        public MyTokenService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<bool> ValidateUser(UserLoginDTO model)
        {
            _user = await _userManager.FindByEmailAsync(model.UserName); //Email adresine göre user nesnesini bul
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, model.Password)); //User nesnesinin pass kontrol et.

            return result;
        }

        //Token Üretimi
        public async Task<string> CreateToken()
        {
            //Kullanicin kimlik bilgilerini al
            var signinCredentials = GetSignCredentials();
            //Kullaninicin rollerini alma
            var claims = await GetClaims();
            //Token seçenekleri
            var tokenOptions = GenerateTokenOptions(signinCredentials, claims);

            //Tokun Oluştur ve Dön
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSignCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            //Username bilgigilerini claim ekle
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };

            //Roller al
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["validIssuer"], audience: jwtSettings["validAudience"], claims: claims, expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])), signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}
