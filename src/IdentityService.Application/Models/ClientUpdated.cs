using System;
using MassTransit;

namespace IdentityService.Application.Models;

[MessageUrn("snack-hub-clients")]
[EntityName("client-updated")]
public record ClientUpdated(Guid Id, string Name, string Cpf, string Email = "");