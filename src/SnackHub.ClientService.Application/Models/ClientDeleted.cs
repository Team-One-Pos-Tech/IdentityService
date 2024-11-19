using System;
using MassTransit;

namespace SnackHub.ClientService.Application.Models;

[MessageUrn("snack-hub-clients")]
[EntityName("client-deleted")]
public record ClientDeleted(Guid Id);