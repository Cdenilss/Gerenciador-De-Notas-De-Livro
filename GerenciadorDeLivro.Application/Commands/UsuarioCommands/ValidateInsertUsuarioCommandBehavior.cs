using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.UsuarioCommands;

public class ValidateInsertUsuarioCommandBehavior : IPipelineBehavior<InsertUsuarioCommand,ResultViewModel<Guid>>
{
    private readonly IUsuarioRepository _repository;

    public ValidateInsertUsuarioCommandBehavior(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<Guid>> Handle(InsertUsuarioCommand request, RequestHandlerDelegate<ResultViewModel<Guid>> next, CancellationToken cancellationToken)
    {
        var email = request.Email;
        var emailExist=await _repository.EmailExiste(email);

        if (emailExist)
        {
            return ResultViewModel<Guid>.Error("Esse email já existe");
        }
        
        return  await next();
    }
}