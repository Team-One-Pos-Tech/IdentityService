using IdentityService.Application.Models;
using System.Threading.Tasks;

namespace IdentityService.Application.Contracts
{
    public interface INotifyUserUseCase
    {
        Task NotifyOrderUpdateStatus(NotifyOrderUpdateStatusRequest request);
    }
}