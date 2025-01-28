using System;
using Flunt.Notifications;

namespace IdentityService.Application.Models;

public class RegisterClientResponse : Notifiable<Notification>
{
    public Guid? Id { get; set; }
}