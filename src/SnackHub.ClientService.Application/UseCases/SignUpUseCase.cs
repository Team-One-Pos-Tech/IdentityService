using System.Threading.Tasks;
using SnackHub.ClientService.Application.Contracts;
using SnackHub.ClientService.Application.Models;
using SnackHub.ClientService.Domain.Models.Gateways;

namespace SnackHub.ClientService.Application.UseCases
{
    public class SignUpUseCase(IRegisterClientUseCase registerClient) : ISignUpUseCase
    {
        public async Task<RegisterClientResponse> Execute(SignUpRequest request)
        {
            var registerClientRequest = new RegisterClientRequest(request.Name, request.Username, request.Email);
            var response = await registerClient.Execute(registerClientRequest);
            
            return response;
        }
    }
}
