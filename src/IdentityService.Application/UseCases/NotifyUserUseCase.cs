using IdentityService.Application.Contracts;
using IdentityService.Application.Models;
using IdentityService.Domain.Contracts;
using System.Threading.Tasks;

namespace IdentityService.Application.UseCases;

public class NotifyUserUseCase(IEmailSender emailSender) : INotifyUserUseCase
{
    public static readonly string OrderStatusUpdateSubject = "Order Status Update";

    public async Task NotifyOrderUpdateStatus(NotifyOrderUpdateStatusRequest request)
    {
        var sendEmailRequest = new SendEmailRequest(
            request.Email,
            OrderStatusUpdateSubject,
            CreateTemplate(request)
        );

        await emailSender.SendEmailAsync(sendEmailRequest);
    }

    private static string CreateTemplate(NotifyOrderUpdateStatusRequest request)
    {
        return $@"
        <html>
            <body>
                <h1>Order Status Update</h1>
                <p>Order ID: {request.OrderId}</p>
                <p>Status: {request.OrderStatus}</p>
                <p>Track your package <a href='{request.PackageUri}'>here</a>.</p>
            </body>
        </html>";
    }
}
