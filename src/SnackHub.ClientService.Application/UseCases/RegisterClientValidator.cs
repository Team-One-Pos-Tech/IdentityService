using System.Threading.Tasks;
using SnackHub.ClientService.Application.Contracts;
using SnackHub.ClientService.Application.Models;
using SnackHub.ClientService.Domain.Contracts;
using SnackHub.ClientService.Domain.ValueObjects;

namespace SnackHub.ClientService.Application.UseCases;

public class RegisterClientValidator : IRegisterClientValidator
{
    private readonly IClientRepository _clientRepository;

    public RegisterClientValidator(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<bool> IsValid(RegisterClientRequest registerClientRequest, RegisterClientResponse response)
    {
        var cpf = new Cpf(registerClientRequest.Cpf);

        if (!cpf.IsValid())
        {
            response.AddNotification("CPF", "CPF is invalid.");
            return false;
        }

        var existsCpf = await _clientRepository.GetClientByCpfAsync(cpf);
        if (existsCpf is not null)
        {
            response.AddNotification("CPF", "CPF is already registered.");
            return false;
        }

        if (string.IsNullOrEmpty(registerClientRequest.Email))
        {
            response.AddNotification("Email", "Email cannot be empty.");
            return false;
        }

        return true;
    }
}