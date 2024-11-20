using Flunt.Notifications;

namespace SnackHub.ClientService.Application.Models;

public class RegisterClientRequest : Notifiable<Notification>
{
    public RegisterClientRequest(string name, string cpf, string email)
    {
        Name = name;
        Cpf = cpf;
        Email = email;
    }

    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
}