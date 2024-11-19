using System;
using MassTransit;

namespace SnackHub.ClientService.Application.Models;

[MessageUrn("snack-hub-clients")]
[EntityName("client-created")]
public record ClientCreated(Guid Id, string Name, string Cpf, string Email = "");