using System.Threading.Tasks;
using IdentityService.Application.Models;
using IdentityService.Domain.Models.Gateways;

namespace IdentityService.Application.Contracts;

public interface ISignInUseCase
{
    Task<SignInResponse> Execute(SignInRequest request);
}