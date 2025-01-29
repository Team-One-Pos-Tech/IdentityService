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
    public async Task NotifyOrderUpdateStatus(NotifyOrderUpdateStatusRequest request)
    {
        var sendEmailRequest = new SendEmailRequest(request.Email, "Order Status Update", "Your order status has been updated.");

        await emailSender.SendEmailAsync(sendEmailRequest);
    }
}
