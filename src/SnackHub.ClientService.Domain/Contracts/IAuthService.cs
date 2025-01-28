using System.Threading.Tasks;
using IdentityService.Domain.Models.Gateways;

namespace IdentityService.Domain.Contracts;

public interface IAuthService
{
    public Task<AuthResponseType> Execute(SignInRequest request);
}