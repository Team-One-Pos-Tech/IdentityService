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
        var packagesText = "";

        if (request.Packages.Length > 0)
            packagesText = GetPackageTemplate(request);

        return $@"
        <html>
            <body>
                <h1>Processing Order Status Update</h1>
                <p>Order ID: {request.OrderId}</p>
                <p>Status: {request.OrderStatus}</p>
                {packagesText}
            </body>
        </html>";
    }

    private static string GetPackageTemplate(NotifyOrderUpdateStatusRequest request)
    {
        var response = "<p>Download your packages below</p>";

        foreach (var package in request.Packages)
        {
            response += $"<p>{package.FileName} <a href='{package.Uri}'>here</a>.</p>";
        }

        return response;
    }
}
