using IdentityService.Api.Extensions;
using IdentityService.Api.Models;
using IdentityService.Application.Contracts;
using IdentityService.Application.Models;
using IdentityService.Domain.Models.Gateways;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityService.Api.Controllers;

/// <summary>
///     Handles user authentication.
/// </summary>
[Route("api/[controller]/v1")]
[ApiController]
public class AuthenticationController(
    ISignInUseCase signInUseCase,
    ISignUpUseCase signUpUseCase) : ControllerBase
{
    private const string DefaultUsersPassword = "Default-password-99!";

    /// <summary>
    ///     Registers a new user
    /// </summary>
    /// <param name="user"></param>
    /// <returns>Action result indicating the outcome of the registration.</returns>
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] RegisterClientRequest user)
    {
        var signUpRequest = new SignUpRequest(
            user.Name,
            user.Cpf,
            DefaultUsersPassword,
            user.Email
        );

        var response = await signUpUseCase.Execute(signUpRequest);

        if (!response.IsValid) return ValidationProblem(ModelState.AddNotifications(response.Notifications));

        return Ok(response);
    }

    /// <summary>
    ///     Authenticates a user
    /// </summary>
    /// <param name="user">To sign in as an Anonymous User the CPF value should be empty</param>
    /// <returns>Action result indicating the outcome of the authentication</returns>
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] LoginModel user)
    {
        var signInRequest = new SignInRequest(user.Cpf, DefaultUsersPassword);

        var response = await signInUseCase.Execute(signInRequest);

        return Ok(response);
    }
}
