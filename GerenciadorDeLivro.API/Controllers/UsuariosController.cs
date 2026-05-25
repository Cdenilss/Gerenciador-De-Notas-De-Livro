using GerenciadorDeLivro.Application.Commands.UsuarioCommands;
using GerenciadorDeLivro.Application.Models.InputModel;
using GerenciadorDeLivro.Application.Queries.UsuarioQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeLivro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{


    private readonly IMediator _mediator;
    
    public UsuariosController( IMediator mediator)
    {
        
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task <IActionResult> GetAll()
    {
        var result =  await _mediator.Send(new GetAllUserQuery());
        
       return Ok(result);
    }

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
    

    [HttpPost]
    public async Task<IActionResult> Post(InsertUsuarioCommand command)
    {
      var result = await _mediator.Send(command);

      if (!result.IsSuccess)
      {
          return BadRequest(result.Message);
      }

      return CreatedAtAction(nameof(GetById),new { id = result.Data }, command);
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(PutUsuarioCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
        {
            NotFound(result.Message);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteUsuarioCommand(id));
        if (!result.IsSuccess)
            return NotFound(result.Message);
        
        return NoContent();
    }
    
    
}
