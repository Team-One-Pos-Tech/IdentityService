using System;
using System.Threading.Tasks;
using SnackHub.ClientService.Application.Models;

namespace SnackHub.ClientService.Application.Contracts;

public interface IGetClientUseCase
{
    Task<GetClientResponse?> Execute(Guid id);
    Task<GetClientResponse?> Execute(string cpf);
}