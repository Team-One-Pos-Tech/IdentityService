using System.Threading.Tasks;
using SnackHub.ClientService.Domain.Models.Gateways;

namespace SnackHub.ClientService.Domain.Contracts;

public interface IAuthService
{
    public Task<AuthResponseType> Execute(SignInRequest request);
}