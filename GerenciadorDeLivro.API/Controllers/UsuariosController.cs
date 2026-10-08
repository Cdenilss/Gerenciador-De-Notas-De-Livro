using GerenciadorDeLivro.Application.Commands.UsuarioCommands;
using GerenciadorDeLivro.Application.Queries.UsuarioQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace GerenciadorDeLivro.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    
    private readonly IMediator _mediator;
    
    public UsuariosController( IMediator mediator)
    {
        
        _mediator = mediator;
    }
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task <IActionResult> GetAll()
    {
        var result =  await _mediator.Send(new GetAllUserQuery());
        
       return Ok(result);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task <IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetByIdUserQuery(id));

        if (!result.IsSuccess)
        {
            return NotFound(result.Message);
        }
       return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Post(InsertUsuarioCommand command)
    {
      var result = await _mediator.Send(command);

      if (!result.IsSuccess)
      {
          return BadRequest(result.Message);
      }

      return CreatedAtAction(nameof(GetById),new { id = result.Data }, result);
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(PutUsuarioCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
        {
            return NotFound(result.Message);
        }
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteUsuarioCommand(id));
        if (!result.IsSuccess)
            return NotFound(result.Message);
        
        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("password-recovery/request")]
    [EnableRateLimiting("password-recovery-request")]

    public async Task<IActionResult> RequestRecoveryPassword(PasswordRecoveryCommand command)
    { 
        await _mediator.Send( command);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("password-recovery/changes")]
    [EnableRateLimiting("password-recovery-change")]
    public async Task<IActionResult> ChangePassword(ChangePassowordCommand command)
    {
       var result= await _mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }
        return NoContent();
    }
    
    
}
