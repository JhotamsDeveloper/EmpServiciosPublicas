using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Contracts.Identity;
using EmpServiciosPublicas.Aplication.Models.Identity;
using EmpServiciosPublicas.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmpServiciosPublicas.Infrastructure.Identity.Services
{
    public class AuthService : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception($"El usuario con email {request.Email} no existe");
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new Exception($"Las credenciales son incorrectas");
            }

            JwtSecurityToken token = await GenerateToken(user);
            AuthResponse authResponse = new()
            {
                Id = user.Id,
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = user.UserName
            };

            return authResponse;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            ApplicationUser userExists = await _userManager.FindByNameAsync(request.Email);
            if (userExists == null)
            {
                throw new Exception($"UserName no esta disponible");
            }

            ApplicationUser emailExists = await _userManager.FindByEmailAsync(request.Email);
            if (emailExists == null)
            {
                throw new Exception($"El email ya se encuentra registrado");
            }

            ApplicationUser user = new()
            {
                Email = request.Email,
                Name = request.Name,
                Surnames = request.Surnames,
                UserName = request.UserName,
                EmailConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Operator");
                JwtSecurityToken token = await GenerateToken(user);
                return new RegistrationResponse
                {
                    UserId = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                };
            }

            throw new Exception($"{result.Errors}");
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser aplicationUser)
        {
            IList<Claim> userClaims = await _userManager.GetClaimsAsync(aplicationUser);
            IList<string> roles = await _userManager.GetRolesAsync(aplicationUser);

            List<Claim> rolesClaims = new();
            foreach (var rol in roles)
            {
                rolesClaims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, aplicationUser.Name),
                new Claim(JwtRegisteredClaimNames.Email, aplicationUser.Email),
                new Claim(CustomClaimTypes.Uid, aplicationUser.Id) // Otro ejemplo de como agregar el key al diccionario

            }.Union(userClaims).Union(rolesClaims);

            SymmetricSecurityKey symmetricSecuritKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecuritKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationMinutes),
                signingCredentials: signingCredentials );

            return jwtSecurityToken;
        }
    }
}
