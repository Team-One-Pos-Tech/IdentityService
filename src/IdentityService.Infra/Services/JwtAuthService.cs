using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityService.Domain.Contracts;
using IdentityService.Domain.Models.Gateways;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Infra.Services;

public class JwtAuthService(IConfiguration configuration) : IAuthService
{
    private readonly IConfiguration _configuration = configuration;

    public async Task<AuthResponseType> Execute(SignInRequest request)
    {
        var serviceKey = _configuration["Auth:Key"] ?? throw new ApplicationException("JWT key is not configured.");

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(serviceKey));
        var issuer = _configuration["Auth:Issuer"];
        var audience = _configuration["Auth:Audience"];

        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokeOptions = new JwtSecurityToken(
            issuer,
            audience,
            new List<Claim>(),
            expires: DateTime.Now.AddMinutes(2),
            signingCredentials: signinCredentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

        return new AuthResponseType(tokenString, true);
    }
}