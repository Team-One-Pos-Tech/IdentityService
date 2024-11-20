using System.Threading.Tasks;
using SnackHub.ClientService.Application.Models;
using SnackHub.ClientService.Domain.Models.Gateways;

namespace SnackHub.ClientService.Application.Contracts;

public interface ISignInUseCase
{
    Task<SignInResponse> Execute(SignInRequest request);
}