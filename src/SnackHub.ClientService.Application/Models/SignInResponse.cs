using Flunt.Notifications;

namespace IdentityService.Application.Models;

public class SignInResponse(string? IdToken) : Notifiable<Notification>
{
    public string? IdToken { get; init; } = IdToken;
}