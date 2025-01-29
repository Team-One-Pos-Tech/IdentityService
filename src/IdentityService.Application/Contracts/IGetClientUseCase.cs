using System;
using System.Threading.Tasks;
using IdentityService.Application.Models;

namespace IdentityService.Application.Contracts;

public interface IGetClientUseCase
{
    Task<GetClientResponse?> GetById(Guid id);
    Task<GetClientResponse?> GetByCpf(string cpf);
}