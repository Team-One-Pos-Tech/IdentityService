using System.Threading.Tasks;
using SnackHub.ClientService.Application.Models;

namespace SnackHub.ClientService.Application.Contracts;

public interface IRegisterClientUseCase
{
    Task<RegisterClientResponse> Execute(RegisterClientRequest registerClientRequest);
}