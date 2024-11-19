using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnackHub.ClientService.Application.Contracts;
using SnackHub.ClientService.Application.Models;

namespace SnackHub.ClientService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1"), Authorize]
    public class ClientController: ControllerBase
    {
        private readonly IGetClientUseCase _getClientUseCase;

        public ClientController(IGetClientUseCase getClientUseCase)
        {
            _getClientUseCase = getClientUseCase;
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetClientResponse>> GetById([FromRoute] Guid id)
        {
            var clientResponse = await _getClientUseCase.Execute(id);
            if (clientResponse is null)
                return NotFound();
            
            return Ok(clientResponse);
        }
        
        [HttpGet("{cpf:minlength(11):maxlength(11)}")]
        public async Task<ActionResult<GetClientResponse>> GetByCpf([FromRoute] string cpf)
        {
            var clientResponse = await _getClientUseCase.Execute(cpf);
            if (clientResponse is null)
                return NotFound();
            
            return Ok(clientResponse);
        }

    }
}