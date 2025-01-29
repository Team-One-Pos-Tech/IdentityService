using IdentityService.Application.Contracts;
using IdentityService.Application.Models;
using IdentityService.Application.UseCases;
using IdentityService.Domain.Contracts;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Models.Gateways;
using MassTransit;
using Moq;

namespace IdentityService.Application.Tests.UseCases
{
    internal class SignUpShould
    {
        private Mock<IClientRepository> clientRepositoryMock;
        private Mock<IRegisterClientValidator> validatorMock;
        private Mock<IPublishEndpoint> publishMock;
        private SignUpUseCase signInUseCase;

        [SetUp]
        public void Setup()
        {
            clientRepositoryMock = new Mock<IClientRepository>();

            validatorMock = new Mock<IRegisterClientValidator>();

            publishMock = new Mock<IPublishEndpoint>();

            validatorMock.Setup(validator => validator.IsValid(It.IsAny<RegisterClientRequest>(), It.IsAny<RegisterClientResponse>()))
                .ReturnsAsync(true);

            signInUseCase = new SignUpUseCase(
                clientRepositoryMock.Object, validatorMock.Object, publishMock.Object);
        }

        [Test]
        public async Task RegisterClient()
        {
            // Arrange

            var request = new SignUpRequest("Ednaldo Pereira", "12345678911", "Default", "email@email.com");

            // Act

            await signInUseCase.Execute(request);

            // Assert

            clientRepositoryMock.Verify(
                gateway => gateway.AddAsync(It.Is<ClientModel>(req => req.Cpf == request.Username)),
                Times.Once
            );

        }

    }
}
