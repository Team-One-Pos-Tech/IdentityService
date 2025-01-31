using System;

namespace IdentityService.Domain.Models.Gateways;

public record SignInRequest
{
    public SignInRequest(string cpf, string password)
    {
        Username = cpf;
        Password = password;
    }

    public string Username { get; set; }
    public string Password { get; set; }
    public Guid UserId { get; set; }
}