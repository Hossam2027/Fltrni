using Fltrni.core.DTO;
using Fltrni.core.Exceptions;
using Fltrni.core.Interface;
using Fltrni.infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.infrastructure.Services
{
    public class AuthService : IAuthService
    {
        public UserManager<AppUser> UserManager { get; }
        public IToken Token { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public AuthService(UserManager<AppUser> userManager, IToken token , RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            Token = token;
            RoleManager = roleManager;
        }

     

        public async Task<object> RegisterAsync(RegisterDto dto)
        {
            var role = dto.Role?.Trim();

            if (role != "Customer" && role != "Provider")
            {
                throw new BadRequestException(
                    "Role must be Customer or Provider.");
            }
            var user = new AppUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                DisplayName = dto.Name
            };
            var result = await UserManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));

                throw new Exception(errors);
            }
            await UserManager.AddToRoleAsync(user, "Customer");
            return new { message = "User created successfully" };
        }

        public async Task<object> LoginAsync(LoginDto dto)
        {
            var user = await UserManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                throw new Exception("Invalid Email or password");
            }

            var checkPassword = await UserManager.CheckPasswordAsync(user, dto.Password);
            if (!checkPassword)
            {
                throw new Exception("Invalid Email or password");
            }
            var roles = await UserManager.GetRolesAsync(user);
            var token = await Token.CreateTokenAsync(user);
            return new { message = "Login successful" , token = token , roles,userName = user.DisplayName
            }; 

        }



        //public async Task CreateAdminAsync(string email)
        //{
        //    var user = await UserManager.FindByEmailAsync(email);

        //    if (user == null)
        //        throw new Exception("User not found");

        //    const string roleName = "Admin";

        //    if (!await RoleManager.RoleExistsAsync(roleName))
        //    {
        //        await RoleManager.CreateAsync(
        //            new IdentityRole(roleName));
        //    }

        //    if (!await UserManager.IsInRoleAsync(user, roleName))
        //    {
        //        await UserManager.AddToRoleAsync(user, roleName);
        //    }
        //}
    }
}
