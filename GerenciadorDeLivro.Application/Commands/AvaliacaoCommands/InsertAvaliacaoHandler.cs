using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.AvaliacaoCommands;

public class InsertAvaliacaoHandler: IRequestHandler<InsertAvaliacaoCommand,ResultViewModel<Guid>>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILivroRepository _livroRepository;
    public InsertAvaliacaoHandler( ILivroRepository livroRepository, IUsuarioRepository usuarioRepository)
    {
        _livroRepository = livroRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<ResultViewModel<Guid>> Handle(InsertAvaliacaoCommand request, CancellationToken cancellationToken)
    {
        var livro = await _livroRepository.GetDetailsById(request.IdLivro);
        if (livro is null)
        {
            return ResultViewModel<Guid>.Error("Livro Não Encontrado");
        }
        var usuarioExiste = await _usuarioRepository.Exist(request.IdUser);
        if (!usuarioExiste)
        {
            return ResultViewModel<Guid>.Error("Usuario Não Encontrado");
        }
        var avaliacao = request.ToEntity();
        livro.AvaliacoesLivro.Add(avaliacao);
        livro.AtualizarNotaMedia();
        await _livroRepository.InsertAvalicao(avaliacao);
        await _livroRepository.Update(livro);
        await _livroRepository.CommitAsync();

        
        return ResultViewModel<Guid>.Success(avaliacao.Id);
    }
}