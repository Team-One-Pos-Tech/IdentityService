using System.Threading.Tasks;
using SnackHub.ClientService.Application.Models;
using SnackHub.ClientService.Domain.Models.Gateways;

namespace SnackHub.ClientService.Application.Contracts;

public interface ISignUpUseCase
{
    Task<RegisterClientResponse> Execute(SignUpRequest request);
}