﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GerenciadorJogos.WebApi.Services
{
    public static class TokenService
    {
        public static string GerarToken(bool? isAdmin)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, RetornarRole(isAdmin))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.ChaveSecreta));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                                                issuer: "desafioinvillia.net",
                                                audience: "desafioinvillia.net",
                                                claims: claims,
                                                expires: Settings.TempoExpiracao,
                                                signingCredentials: creds
                                            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string RetornarRole(bool? isAdmin)
        {
            return isAdmin == true ? "Admin" : "User";
        }
    }
}
