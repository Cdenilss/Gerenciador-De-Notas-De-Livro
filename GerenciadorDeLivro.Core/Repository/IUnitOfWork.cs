namespace GerenciadorDeLivro.Core.Repository;

public interface IUnitOfWork
{
    ILivroRepository Livro { get; }
    IUsuarioRepository Usuario { get; }
    IAvaliacaoRepository Avaliacao { get; }

    Task<int> CompleteAsync( CancellationToken cancellationToken = default);
}