using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IdentityService.Application.Contracts;
using IdentityService.Application.Models;
using IdentityService.Application.UseCases;
using IdentityService.Behavior.Tests.Fixtures;
using IdentityService.Domain.Contracts;
using IdentityService.Domain.Models.Gateways;
using IdentityService.Infra.Repositories;
using IdentityService.Infra.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Moq;
using Reqnroll;

namespace IdentityService.Behavior.Tests.StepDefintions;

[Binding]
public class SignUpSignInStepDefinitions : PostgreSqlFixture
{
    private ISignUpUseCase _signUpUseCase;
    private ISignInUseCase _signInUseCase;
    private IGetClientUseCase _getClientUseCase;

    private IClientRepository _clientRepository;

    private Mock<IPublishEndpoint> _publishEndpointMock;

    private SignUpRequest _signUpRequest;

    [BeforeScenario]
    public async Task Setup()
    {
        await BaseSetUp();
        _publishEndpointMock = new Mock<IPublishEndpoint>();

        _clientRepository = new ClientRepository(ClientDbContext);
        var registerValidator = new RegisterClientValidator(_clientRepository);

        _signUpUseCase = new SignUpUseCase(_clientRepository, registerValidator, _publishEndpointMock.Object);
        _getClientUseCase = new GetClientUseCase(_clientRepository);

        var configDictionary = new Dictionary<string, string>
        {
            {"Auth:Key", "andomlyGeneratedKey12345randomlyGeneratedKey12345randomlyGeneratedKey12345randomlyGeneratedKey12345"},
            {"Auth:Issuer", "RandomIssuer"},
            {"Auth:Audience", "RandomAudience"},
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configDictionary)
            .Build();

        var authService = new FakeJwtAuthService(configuration);

        _signInUseCase = new SignInUseCase(authService, _getClientUseCase);
    }

    [Given("a SignUpRequest with name '(.*)', cpf '(.*)', password '(.*)' and email '(.*)'")]
    public void GivenASignUpRequestWithNameCpfAndEmail(string name, string cpf, string password, string email)
    {
        _signUpRequest = new SignUpRequest(name, cpf, password, email);
    }

    [When("persisting it into the database")]
    public async Task WhenPersistingItIntoTheDatabase()
    {
        await _signUpUseCase.Execute(_signUpRequest);
    }

    [Then("the client with cpf '(.*)' should be stored")]
    public async Task ThenItShouldBeStored(string cpf)
    {
        var client = await _getClientUseCase.Execute(cpf);

        client
            .Should()
            .NotBeNull();

        client
            .Cpf
            .Should()
            .NotBeNull();

        client
            .Cpf
            .Value
            .Should()
            .Be(cpf);
    }

    [Then("an event of type 'ClientCreated' is raised")]
    public void ThenProductCreatedEventIsRaised()
    {
        _publishEndpointMock
            .Verify(publishEndpoint => publishEndpoint.Publish(It.IsAny<ClientCreated>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Then("it should be able to sign in by using cpf '(.*)' and password '(.*)")]
    public async Task ThenItShouldBeAbleToSignInWithAValidToken(string cpf, string password)
    {
        var signInRequest = new SignInRequest(cpf, password);
        var signInResponse = await _signInUseCase.Execute(signInRequest);

        signInResponse
            .Should()
            .NotBeNull();

        signInResponse
            .IdToken
            .Should()
            .NotBeEmpty();
    }
}