using SnackHub.ClientService.Domain.ValueObjects;

namespace SnackHub.ClientService.Application.Models;

public record GetClientResponse(string Name, Cpf Cpf);