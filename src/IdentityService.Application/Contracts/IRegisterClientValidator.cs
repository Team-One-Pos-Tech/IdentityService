using System.Threading.Tasks;
using IdentityService.Application.Models;

namespace IdentityService.Application.Contracts;

public interface IRegisterClientValidator
{
    Task<bool> IsValid(RegisterClientRequest registerClientRequest, RegisterClientResponse response);
}