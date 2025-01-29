using FluentAssertions;
using IdentityService.Application.UseCases;
using IdentityService.Domain.Contracts;
using IdentityService.Domain.Entities;
using Moq;

namespace IdentityService.Application.Tests.UseCases;

public class GetClientShould
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Get()
    {
        // Arrange
        var mockClientRepository = new Mock<IClientRepository>();

        var getClientUseCase = new GetClientUseCase(mockClientRepository.Object);

        var id = Guid.NewGuid();

        var clientMock = ClientModel.Create(id, "Ednaldo Pereira", "72860763023", "");

        mockClientRepository.Setup(repository => repository.GetClientByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(clientMock);

        // Act
        var response = await getClientUseCase.Execute(id);

        // Assert
        response
            .Should()
            .NotBeNull();

        response!
            .Name
            .Should()
            .Be(clientMock.Name);

        response
            .Cpf
            .Value
            .Should()
            .Be(clientMock.Cpf);

    }
}