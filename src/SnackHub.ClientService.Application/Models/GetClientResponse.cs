using IdentityService.Domain.ValueObjects;

namespace IdentityService.Application.Models;

public record GetClientResponse(string Name, Cpf Cpf);