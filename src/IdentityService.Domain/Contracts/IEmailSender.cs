﻿using System.Threading.Tasks;

namespace IdentityService.Domain.Contracts;

public record SendEmailRequest(string Recipient, string Subject, string Body);

public interface IEmailSender
{
    Task SendEmailAsync(SendEmailRequest sendMailRequest);
}
