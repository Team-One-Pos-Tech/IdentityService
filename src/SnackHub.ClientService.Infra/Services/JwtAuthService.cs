using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SnackHub.ClientService.Domain.Contracts;
using SnackHub.ClientService.Domain.Models.Gateways;

namespace SnackHub.ClientService.Infra.Services;

public class JwtAuthService(IConfiguration configuration): IAuthService
{
    public Task<AuthResponseType> Execute(SignInRequest request)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Key"] ??
                throw new ApplicationException("JWT key is not configured.")));
        var issuer = configuration["Auth:Issuer"];
        var audience = configuration["Auth:Audience"];
                        
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokeOptions = new JwtSecurityToken(
            issuer : issuer,
            audience: audience,
            claims: new List<Claim>(),
            expires: DateTime.Now.AddMinutes(2),
            signingCredentials: signinCredentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

        return Task.FromResult(new AuthResponseType(tokenString, true));
    }
}