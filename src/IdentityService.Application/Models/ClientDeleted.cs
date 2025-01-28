using System;
using MassTransit;

namespace IdentityService.Application.Models;

[MessageUrn("snack-hub-clients")]
[EntityName("client-deleted")]
public record ClientDeleted(Guid Id);