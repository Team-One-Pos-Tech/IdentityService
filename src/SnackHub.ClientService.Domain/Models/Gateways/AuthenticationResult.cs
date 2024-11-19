namespace SnackHub.ClientService.Domain.Models.Gateways;

public class AuthenticationResult
{
    public string IdToken { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}