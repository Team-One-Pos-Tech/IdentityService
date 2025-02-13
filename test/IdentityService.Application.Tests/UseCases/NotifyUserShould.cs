﻿using IdentityService.Application.Models;
using IdentityService.Application.UseCases;
using IdentityService.Domain.Contracts;
using Moq;

namespace IdentityService.Application.Tests.UseCases;

internal class NotifyUserShould
{
    private Mock<IEmailSender> emailSenderMock;
    private NotifyUserUseCase notifyUser;

    [SetUp]
    public void Setup()
    {
        emailSenderMock = new Mock<IEmailSender>();

        notifyUser = new NotifyUserUseCase(emailSenderMock.Object);
    }

    [Test]
    public async Task Send_Email_To_User()
    {
        // Arrange
        var request = new NotifyOrderUpdateStatusRequest
        {
            Email = "email@mail.com",
            OrderId = Guid.NewGuid(),
            OrderStatus = "Concluded",
            Packages = [
                new PackageRequest("package1", "http://url.com"),
            ],
        };

        // Act
        await notifyUser.NotifyOrderUpdateStatus(request);

        // Assert
        emailSenderMock.Verify(x => x.SendEmailAsync(
            It.Is<SendEmailRequest>(rq => rq.Recipient == request.Email)), Times.Once);
    }

    [Test]
    public async Task Send_Email_With_Subject()
    {
        // Arrange
        var request = new NotifyOrderUpdateStatusRequest
        {
            Email = "email@mail.com",
            OrderId = Guid.NewGuid(),
            OrderStatus = "Concluded",
            Packages = [
                new PackageRequest("package1", "http://url.com"),
            ],
        };

        // Act
        await notifyUser.NotifyOrderUpdateStatus(request);

        // Assert
        emailSenderMock.Verify(x => x.SendEmailAsync(
            It.Is<SendEmailRequest>(rq => rq.Subject == NotifyUserUseCase.OrderStatusUpdateSubject)), Times.Once);
    }

    [Test]
    public async Task Send_Email_With_Order_Id()
    {
        // Arrange
        var request = new NotifyOrderUpdateStatusRequest
        {
            Email = "email@mail.com",
            OrderId = Guid.NewGuid(),
            OrderStatus = "Concluded",
            Packages = [
                new PackageRequest("package1", "http://url.com"),
            ],
        };

        // Act
        await notifyUser.NotifyOrderUpdateStatus(request);

        // Assert
        emailSenderMock.Verify(x => x.SendEmailAsync(
            It.Is<SendEmailRequest>(rq => rq.Body.Contains(request.OrderId.ToString()))), Times.Once);
    }

    [Test]
    public async Task Send_Email_With_Package_Uri_When_Status_Is_Concluded()
    {
        // Arrange
        var request = new NotifyOrderUpdateStatusRequest
        {
            Email = "email@mail.com",
            OrderId = Guid.NewGuid(),
            OrderStatus = "Concluded",
            Packages = [
                new PackageRequest("package1", "http://url.com"),
            ],
        };

        // Act
        await notifyUser.NotifyOrderUpdateStatus(request);

        // Assert
        emailSenderMock.Verify(x => x.SendEmailAsync(
            It.Is<SendEmailRequest>(rq => rq.Body.Contains(request.Packages.First().FileName))), Times.Once);
    }
}
