using Fltrni.core.Interface;
using Fltrni.infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.infrastructure.Services
{
    public class Token : IToken
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public Token(IConfiguration configuration , UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<string> CreateTokenAsync(AppUser appUser)
        {
            //  إنشاء قائمة Claims
            var claims = new List<Claim>
            {
             new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                new Claim(ClaimTypes.Name, appUser.UserName),
                new Claim(ClaimTypes.Email, appUser.Email)
            };
            // 1. إنشاء مفتاح التشفير
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])
);
            // 2. جلب Roles الخاصة بالمستخدم
            var roles = await _userManager.GetRolesAsync(appUser);

            // 3. إضافة Roles داخل Token
            foreach (var role in roles)
            {
                claims.Add(
                    new Claim(ClaimTypes.Role, role)
                );
            }
            // 4. إنشاء SigningCredentials
            var credentials = new SigningCredentials(
             key,
             SecurityAlgorithms.HmacSha256
         );
            // 5. إنشاء Token
            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            // 6. تحويل Token إلى سلسلة نصية
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
    }

