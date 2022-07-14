using EmpServiciosPublicas.Aplication.Contracts.Identity;
using EmpServiciosPublicas.Aplication.Models.Identity;
using EmpServiciosPublicas.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace EmpServiciosPublicas.Infrastructure.Identity.Services
{
    public class AuthService : IAuthServices
    {
        private readonly UserManager<AplicationUser> _userManager;
        private readonly SignInManager<AplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<AplicationUser> userManager, SignInManager<AplicationUser> signInManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            AplicationUser user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception($"El usuario con email {request.Email} no existe");
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new Exception($"Las credenciales son incorrectas");
            }

            AuthResponse authResponse = new()
            {
                Id = user.Id,
                Email = user.Email,
                Token = "",
                UserName = user.UserName
            };

            return authResponse;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            AplicationUser userExists = await _userManager.FindByNameAsync(request.Email);
            if (userExists == null)
            {
                throw new Exception($"UserName no esta disponible");
            }

            AplicationUser emailExists = await _userManager.FindByEmailAsync(request.Email);
            if (emailExists == null)
            {
                throw new Exception($"El email ya se encuentra registrado");
            }

            AplicationUser user = new()
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
                return new RegistrationResponse
                {
                    UserId = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Token = ""
                };
            }

            throw new Exception($"{result.Errors}");
        }
    }
}
