using System;
using System.Threading.Tasks;
using IdentityService.Application.Contracts;
using IdentityService.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class ClientController : ControllerBase
{
    private readonly IGetClientUseCase _getClientUseCase;

    public ClientController(IGetClientUseCase getClientUseCase)
    {
        _getClientUseCase = getClientUseCase;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetClientResponse>> GetById([FromRoute] Guid id)
    {
        var clientResponse = await _getClientUseCase.GetById(id);
        if (clientResponse is null)
            return NotFound();

        return Ok(clientResponse);
    }

    [HttpGet("{cpf:minlength(11):maxlength(11)}")]
    public async Task<ActionResult<GetClientResponse>> GetByCpf([FromRoute] string cpf)
    {
        var clientResponse = await _getClientUseCase.GetByCpf(cpf);
        if (clientResponse is null)
            return NotFound();

        return Ok(clientResponse);
    }
}