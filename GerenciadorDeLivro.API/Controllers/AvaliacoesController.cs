using GerenciadorDeLivro.Application.Queries.AvaliacaoQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeLivro.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AvaliacoesController: ControllerBase
{
    public AvaliacoesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    private readonly IMediator _mediator;
    
    [HttpGet]
    public async Task< IActionResult> Get()
    {
        var result = await _mediator.Send(new GetAllAvaliacaoQuery());

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Data);
    }

    [HttpGet("{id}/avaliacoes")]

    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetAvalicaoByIdQuery(id));
        if (!result.IsSuccess)
        {
            return NotFound(result.Message);
        }
        
        return Ok(result.Data);
    }
    
}

