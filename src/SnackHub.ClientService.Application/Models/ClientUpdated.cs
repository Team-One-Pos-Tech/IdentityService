using System;
using MassTransit;

namespace SnackHub.ClientService.Application.Models;

[MessageUrn("snack-hub-clients")]
[EntityName("client-updated")]
public record ClientUpdated(Guid Id, string Name, string Cpf, string Email = "");