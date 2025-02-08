using IdentityService.Application.Contracts;
using IdentityService.Application.Models;
using IdentityService.Application.Models.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Application.Consumers;

public class NotifyUpdateOrderConsumer(
    ILogger<NotifyUpdateOrderConsumer> logger,
    INotifyUserUseCase notifyUserUseCase,
    IGetClientUseCase getClientUseCase) : IConsumer<OrderStatusChangedEvent>
{
    public async Task Consume(ConsumeContext<OrderStatusChangedEvent> context)
    {
        var user = await getClientUseCase.GetById(context.Message.OwnerId);

        if (user == null)
        {
            logger.LogError("User not found for order {OrderId}", context.Message.Parameters.OrderId);
            return;
        }

        await notifyUserUseCase.NotifyOrderUpdateStatus(new NotifyOrderUpdateStatusRequest
        {
            Email = user.Email,
            OrderId = context.Message.Parameters.OrderId,
            OrderStatus = context.Message.Parameters.Status,
            Packages = GetPackages(context)
        });

        logger.LogInformation(
            "User {UserId} notified for order {OrderId} status update",
            context.Message.OwnerId,
            context.Message.Parameters.OrderId);
    }

    private static PackageRequest[] GetPackages(ConsumeContext<OrderStatusChangedEvent> context)
    {
        return context.Message.Parameters.Packages
            .Select(package => new PackageRequest(package.FileName, package.Uri)).ToArray();
    }
}
