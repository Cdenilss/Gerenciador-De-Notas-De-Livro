using GerenciadorDeLivro.Application.Commands.AvaliacaoCommands;
using GerenciadorDeLivro.Application.Commands.LivrosCommands;
using GerenciadorDeLivro.Application.Queries.LivroQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeLivro.API.Controllers;
[Route("api/[controller]")]
[ApiController]

public class LivrosController : ControllerBase
{
    private readonly IMediator _mediator;

    public LivrosController(  IMediator mediator)
    {
        _mediator = mediator;
    }
  
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        
        var query = new GetAllQuery();
        var result = await _mediator.Send(query);
        
        return Ok(result);
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetByIdQuery(id));

        if (!result.IsSuccess)
        {
            return NotFound(result.Message);
        }
        
        return Ok(result);
    }

    [HttpPost]

    public async Task<IActionResult> Post(InsertLivroCommand command)
    {
     var result= await _mediator.Send(command);
     
     if (!result.IsSuccess)
     {
         return BadRequest(result.Message);
         
     }
     return CreatedAtAction(nameof(GetById), new {id= result.Data}, command);
    }
    [HttpPost("/api/livros/{idLivro}/avaliacoes")] 
    public async Task<IActionResult> PostAvaliacao(Guid idLivro, InsertAvaliacaoCommand command)
    {
        
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }
        return CreatedAtAction(nameof(GetById), new {id= result.Data}, result);
    }
    
    [HttpDelete("/api/livros/{id}")]
    public async Task<IActionResult>  Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteLivroCommand(id));
        if (!result.IsSuccess)
        {
            return NotFound(result.Message);
        }
        return NoContent();
    }
    
    
}
