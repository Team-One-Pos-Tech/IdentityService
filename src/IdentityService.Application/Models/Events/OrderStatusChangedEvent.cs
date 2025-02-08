using MassTransit;
using System;

namespace IdentityService.Application.Models.Events;

[MessageUrn("frameup-order-service")]
[EntityName("order-status-changed")]
public record OrderStatusChangedEvent(Guid OwnerId, OrderStatusChangedEventParameters Parameters);

public record OrderStatusChangedEventParameters
{
    public Guid OrderId { get; set; }
    public required string Status { get; set; }
    public PackageItemResponse[] Packages { get; set; } = [];
}

public record PackageItemResponse(string FileName, string Uri);