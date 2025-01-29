using IdentityService.Application.Contracts;
using IdentityService.Application.Models;
using IdentityService.Domain.Contracts;
using System.Threading.Tasks;

namespace IdentityService.Application.UseCases;

public class NotifyUserUseCase(IEmailSender emailSender) : INotifyUserUseCase
{
    public static readonly string OrderStatusUpdateSubject = "Processing Order Status Update";

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
        var packageUriText = GetPackageUriText(request);

        return $@"
        <html>
            <body>
                <h1>Processing Order Status Update</h1>
                <p>Order ID: {request.OrderId}</p>
                <p>Status: {request.OrderStatus}</p>
                {packageUriText}
            </body>
        </html>";
    }

    private static string GetPackageUriText(NotifyOrderUpdateStatusRequest request)
    {
        if(request.OrderStatus == "Concluded")
            return $"<p>Download your package <a href='{request.PackageUri}'>here</a>.</p>";

        return $"";
    }
}
