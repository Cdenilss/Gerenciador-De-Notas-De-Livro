using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.AvaliacaoCommands;

public class ValidateInsertAvaliacaoCommandBehavior:  IPipelineBehavior<InsertAvaliacaoCommand,ResultViewModel<Guid>>
{
    private readonly IAvaliacaoRepository _repository;

    public ValidateInsertAvaliacaoCommandBehavior(IAvaliacaoRepository repository)
    {
        _repository = repository;
    }
  
    public async Task<ResultViewModel<Guid>> Handle(InsertAvaliacaoCommand request, RequestHandlerDelegate<ResultViewModel<Guid>> next, CancellationToken cancellationToken)
     {
         var userJaAvaliouLivro= await _repository.ExistsAvaliacaoByUserId(request.IdUser,request.IdLivro);

         if (userJaAvaliouLivro)
         {
             return ResultViewModel<Guid>.Error("Este usuario ja possui avaliação neste livro");
         }
    
    return await next();
     }
    
    
}