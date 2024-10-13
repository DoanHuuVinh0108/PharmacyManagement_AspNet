using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Response;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.DTO;
using PharmacyManagermentSystem.Services.MiniServiceCaching;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PharmacyManagermentSystem.Services.MiniServiceAuth
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ICachingService _cachingService;
        private readonly MyDbContext _dbcontext;
        public AuthService(SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration, ICachingService cachingService, MyDbContext dbcontext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _cachingService = cachingService;
            _dbcontext = dbcontext;
        }

        private string CreateJwt(User user, IEnumerable<string> roles, DateTime time, bool RefreshToken)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? "")
            };

            if (RefreshToken)
            {
                claims.Add(new Claim("RefreshToken", "Refresh_Token"));
            }

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Key"] ?? ""));

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: time,
                signingCredentials: new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
        public async Task<JwtResponse?> Login(SignInRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Email);
            if(user == null)
            {
                throw new Exception("User Not Found");
            }
            
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
            
            if (result.Succeeded)
            {
                var userResult = await _userManager.FindByEmailAsync(request.Email);
                if(userResult != null)
                {
                    var roles = await _userManager.GetRolesAsync(userResult);
                    var time = DateTime.UtcNow.AddMinutes(30);
                    var accessToken = CreateJwt(userResult, roles, DateTime.Now.AddMinutes(6), false);
                    var refreshToken = CreateJwt(userResult, roles, DateTime.Now.AddDays(1), true);
                    return new JwtResponse
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                        UserId = userResult.Id,
                        Roles = roles
                    };
                }
                throw new Exception("User Not Found");
            }
            return null;
        }
        private string? ValidateToken(string token, bool validateLifetime, bool isRefreshToken)
        {
            var parameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = validateLifetime,
                ValidAudience = _configuration["JWT:Audience"],
                ValidIssuer = _configuration["JWT:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Key"] ?? "")),
                ClockSkew = TimeSpan.Zero,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = (JwtSecurityToken)securityToken;

            var versionClaim = principal.FindFirstValue(ClaimTypes.Version);
            if (isRefreshToken && versionClaim != "Refresh_Token")
            {
                throw new Exception("Mã không hợp lệ");
            }

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Mã không hợp lệ");

            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public async Task<TokenModel> RefreshToken(TokenModel token, bool isExtension)
        {
            var userId = ValidateToken(token.RefreshToken, true, true);
            if (userId == null)
            {
                throw new Exception("Mã không hợp lệ");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Không tìm thấy user");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var access_token = CreateJwt(user, roles, DateTime.Now.AddMinutes(5), false);

            return new TokenModel
            {
                AccessToken = access_token
            };
        }
        //public async Task Logout(TokenModel token)
        //{
        //    var userId = ValidateToken(token.RefreshToken, false, true);
        //    if (userId == null)
        //    {
        //        throw new Exception("Mã không hợp lệ");
        //    }
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        throw new Exception("Không tìm thấy user");
        //    }
        //}
        public bool VerifyResetToken(string email, string token)
        {
            var cachedToken = _cachingService.Get<string>(email);
            if (cachedToken == null || cachedToken != token) return false;
            else
            {
                return true;
            }
        }
    }
}
