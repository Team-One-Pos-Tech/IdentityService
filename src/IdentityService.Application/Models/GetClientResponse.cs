using IdentityService.Domain.ValueObjects;
using System;

namespace IdentityService.Application.Models;

public record GetClientResponse(string Name, Cpf Cpf)
{
    public string Email { get; internal set; }
    public Guid Id { get; internal set; }
}
