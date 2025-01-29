using System;

namespace IdentityService.Application.Models;

public class NotifyOrderUpdateStatusRequest
{
    public required string Email { get; set; }
    public Guid OrderId { get; set; }
    public required string OrderStatus { get; set; }
    public string? PackageUri { get; set; }
}