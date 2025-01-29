using System;

namespace IdentityService.Application.Models;

public class NotifyOrderUpdateStatusRequest
{
    public required string Email { get; set; }
    public Guid OrderId { get; set; }
    public string OrderStatus { get; set; }
}