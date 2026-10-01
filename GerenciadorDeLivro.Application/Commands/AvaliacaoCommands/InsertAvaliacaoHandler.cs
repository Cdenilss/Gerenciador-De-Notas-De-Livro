using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.AvaliacaoCommands;

public class InsertAvaliacaoHandler: IRequestHandler<InsertAvaliacaoCommand,ResultViewModel<Guid>>
{

    private readonly IUnitOfWork _unitOfWork;
    public InsertAvaliacaoHandler(  IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel<Guid>> Handle(InsertAvaliacaoCommand request, CancellationToken cancellationToken)
    {
        var livro = await _unitOfWork.Livro.GetDetailsById(request.IdLivro);
        if (livro is null)
        {
            return ResultViewModel<Guid>.Error("Livro Não Encontrado");
        }
        var usuarioExiste = await _unitOfWork.Usuario.Exist(request.IdUser);
        if (!usuarioExiste)
        {
            return ResultViewModel<Guid>.Error("Usuário não encontrado");
        }
        var avaliacao = request.ToEntity();

        livro.AvaliacoesLivro.Add(avaliacao);
        livro.AtualizarNotaMedia();
        await _unitOfWork.Livro.InsertAvaliacao(avaliacao);
        await _unitOfWork.Livro.Update(livro);
        await _unitOfWork.CompleteAsync();

        return ResultViewModel<Guid>.Success(avaliacao.Id);
    }
}
