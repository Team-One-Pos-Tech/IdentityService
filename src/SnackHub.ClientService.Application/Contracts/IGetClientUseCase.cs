using System;
using System.Threading.Tasks;
using IdentityService.Application.Models;

namespace IdentityService.Application.Contracts;

public interface IGetClientUseCase
{
    Task<GetClientResponse?> Execute(Guid id);
    Task<GetClientResponse?> Execute(string cpf);
}