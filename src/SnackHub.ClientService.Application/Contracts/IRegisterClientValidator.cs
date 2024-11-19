using System.Threading.Tasks;
using SnackHub.ClientService.Application.Models;

namespace SnackHub.ClientService.Application.Contracts
{
    public interface IRegisterClientValidator
    {
        Task<bool> IsValid(RegisterClientRequest registerClientRequest, RegisterClientResponse response);
    }
}