using IdentityService.Application.Models;
using IdentityService.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.UseCases;

public class NotifyUserUseCase(IEmailSender emailSender)
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
        return $"{request.OrderId}, {request.OrderStatus}, {request.PackageUri}";
    }
}
