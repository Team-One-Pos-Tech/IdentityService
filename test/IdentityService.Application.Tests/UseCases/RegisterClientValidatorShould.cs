﻿using Moq;
using IdentityService.Application.Contracts;
using IdentityService.Domain.Contracts;
using IdentityService.Application.UseCases;
using IdentityService.Application.Models;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Tests.UseCases
{
    public class RegisterClientValidatorShould
    {
        IRegisterClientValidator _registerClientValidator;
        Mock<IClientRepository> _clientReposioryMock;

        [SetUp]
        public void Setup()
        {
            _clientReposioryMock = new Mock<IClientRepository>();
            _registerClientValidator = new RegisterClientValidator(_clientReposioryMock.Object);
        }

        [Test]
        public async Task Validate_With_Valid_CPFAsync()
        {
            // Arrange

            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "728.607.630-23", "");

            registerClientRequest.Email = "email";

            var response = new RegisterClientResponse();

            // Act
            var isValid = await _registerClientValidator.IsValid(registerClientRequest, response);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        public async Task Validate_Invalid_CPFAsync()
        {
            // Arrange

            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "111.111.111-23", "");

            var response = new RegisterClientResponse();

            // Act
            var isValid = await _registerClientValidator.IsValid(registerClientRequest, response);

            // Assert
            Assert.That(isValid, Is.False);

            Assert.Multiple(() =>
            {
                Assert.That(response.Notifications.First().Key, Is.EqualTo("CPF"));
                Assert.That(response.Notifications.First().Message, Is.EqualTo("CPF is invalid."));
            });
        }

        [Test]
        public async Task Validate_If_CPF_Is_Already_Registred()
        {
            // Arrange
            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "728.607.630-23", "");
            var response = new RegisterClientResponse();

            var cpf = new Domain.ValueObjects.Cpf(registerClientRequest.Cpf);

            _clientReposioryMock.Setup(repository => repository
                .GetClientByCpfAsync(cpf))
                    .ReturnsAsync(
                        ClientModel.Create(
                            Guid.NewGuid(),
                            registerClientRequest.Name,
                            "728.607.630-23",
                            "")
                        );

            // Act
            var isValid = await _registerClientValidator.IsValid(registerClientRequest, response);

            // Assert
            Assert.IsFalse(isValid);

            Assert.Multiple(() =>
            {
                Assert.That(response.Notifications.First().Key, Is.EqualTo("CPF"));
                Assert.That(response.Notifications.First().Message, Is.EqualTo("CPF is already registered."));
            });

        }

        [Test]
        public async Task Validate_Empty_Email()
        {
            // Arrange

            var registerClientRequest = new RegisterClientRequest(name: "John Doe", cpf: "728.607.630-23", "");

            var response = new RegisterClientResponse();

            // Act
            var isValid = await _registerClientValidator.IsValid(registerClientRequest, response);

            // Assert
            Assert.That(isValid, Is.False);

            Assert.Multiple(() =>
            {
                Assert.That(response.Notifications.First().Key, Is.EqualTo("Email"));
                Assert.That(response.Notifications.First().Message, Is.EqualTo("Email cannot be empty."));
            });
        }
    }
}
