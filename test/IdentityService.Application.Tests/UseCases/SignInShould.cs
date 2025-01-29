﻿using FluentAssertions;
using IdentityService.Application.Contracts;
using IdentityService.Application.Models;
using IdentityService.Application.UseCases;
using IdentityService.Domain.Contracts;
using IdentityService.Domain.Models.Gateways;
using IdentityService.Domain.ValueObjects;
using Moq;

namespace IdentityService.Application.Tests.UseCases
{
    internal class SignInShould
    {
        private Mock<IGetClientUseCase> mockGetClient;
        private SignInUseCase signInUseCase;
        private Mock<IAuthService> mockSignInFunctionGateway;

        [SetUp]
        public void Setup()
        {
            mockGetClient = new Mock<IGetClientUseCase>();

            mockSignInFunctionGateway = new Mock<IAuthService>();

            signInUseCase = new SignInUseCase(mockSignInFunctionGateway.Object, mockGetClient.Object);
        }

        [Test]
        public async Task AuthenticateUser()
        {
            // Arrange
            mockSignInFunctionGateway
                .Setup(gateway => gateway.Execute(It.IsAny<SignInRequest>()))
                .ReturnsAsync(new AuthResponseType("token", true));

            mockGetClient
                .Setup(getClient => getClient.Execute(It.IsAny<string>()))
                .ReturnsAsync(new GetClientResponse("Maycon Jordan", new Cpf("72860763023")));

            var request = new SignInRequest("72860763023", "123");

            // Act

            SignInResponse response = await signInUseCase.Execute(request);

            // Assert

            response
                .Should()
                .NotBeNull();

            response
                .IdToken
                .Should()
                .NotBeNull();

        }

        [Test]
        public async Task Dont_Auth_When_Does_Not_Find_User()
        {
            // Arrange

            mockGetClient
                .Setup(getClient => getClient.Execute(It.IsAny<string>()));

            var request = new SignInRequest("72860763023", "123");

            // Act

            SignInResponse response = await signInUseCase.Execute(request);

            // Assert

            response
                .IdToken
                .Should()
                .BeNull();

            response.Notifications.First().Message.Should()
                .Be("User not found");
        }

        [Test]
        public async Task Authenticate_Anonymous_User_When_Cpf_Is_Empty()
        {
            // Arrange

            var anonymousUsername = "00000000000";

            mockSignInFunctionGateway
                .Setup(gateway => gateway.Execute(It.IsAny<SignInRequest>()))
                .ReturnsAsync(new AuthResponseType("token", true));

            var request = new SignInRequest("", "DefaultPassword");

            // Act

            SignInResponse response = await signInUseCase.Execute(request);

            // Assert

            mockSignInFunctionGateway.Verify(
                gateway => gateway.
                    Execute(It.Is<SignInRequest>(req => req.Username == "")),
                Times.Once
            );

            response
                .Should()
                .NotBeNull();

            response
                .IdToken
                .Should()
                .NotBeNull();

        }
    }
}
