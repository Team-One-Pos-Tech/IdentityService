using System;
using Flunt.Notifications;

namespace SnackHub.ClientService.Application.Models
{
    public class RegisterClientResponse() : Notifiable<Notification>
    {
        public Guid? Id { get; set; }

    };
}