using System;
using MassTransit;

namespace IdentityService.Application.Models;

[MessageUrn("snack-hub-clients")]
[EntityName("client-created")]
public record ClientCreated(Guid Id, string Name, string Cpf, string Email = "");