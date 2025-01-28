using System.Threading.Tasks;
using IdentityService.Application.Models;
using IdentityService.Domain.Models.Gateways;

namespace IdentityService.Application.Contracts;

public interface ISignUpUseCase
{
    Task<RegisterClientResponse> Execute(SignUpRequest request);
}