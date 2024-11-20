using Flunt.Notifications;

namespace SnackHub.ClientService.Application.Models;

public class SignInResponse(string? IdToken) : Notifiable<Notification>
{
    public string? IdToken { get; init; } = IdToken;
}