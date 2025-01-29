namespace IdentityService.Api.Configuration;

public record EmailServiceSettings
{
    public required string Host { get; set; }
    public required int Port { get; set; }
    public required string User { get; set; }
    public required string Pass { get; set; }
}