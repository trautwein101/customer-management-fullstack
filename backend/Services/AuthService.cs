using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CustomerManagement.Api.Auth;
using CustomerManagement.Api.DTOs;
using CustomerManagement.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CustomerManagement.Api.Services
{
    public class AuthService : IAuthService
    {

        public readonly DemoUserStore _userStore;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<DemoUser> _passwordHasher = new();


        public AuthService(
            DemoUserStore userStore,
            IConfiguration configuration)
        {
            _userStore = userStore;
            _configuration = configuration;
        }


        public LoginResponseDto? Login(LoginDto loginDto)
        {
            //1. Find the user
            var user = _userStore.FindByUsername(loginDto.Username);

            if (user == null)
                return null;

            //2. Verify the submitted password
            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                loginDto.Password);

            if (result == PasswordVerificationResult.Failed)
                return null;

            //3. Read the JWT configuration
            var key = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT key is missing.");

            var issuer = _configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException("JWT issuer is missing.");

            var audience = _configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException("JWT audience is missing.");

            //4. Create the signing credentials
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key));

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

            //5. Create the claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };


            //6. Generate JWT
            var expiresAt = DateTime.UtcNow.AddMinutes(60);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);


            //7. Return the token
            return new LoginResponseDto
            {
                AccessToken = new JwtSecurityTokenHandler()
                    .WriteToken(token),
                    ExpiresAt = expiresAt
            };



        }


    }
}
