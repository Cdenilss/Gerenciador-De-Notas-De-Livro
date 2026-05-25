using GerenciadorDeLivro.Core.Entities;

namespace GerenciadorDeLivro.Core.Repository;

public interface ILivroRepository
{
    Task<List<Livro>> GetAll();
    Task<Livro?> GetById(Guid id);
    Task<Livro?>GetDetailsById(Guid id);
    Task<bool>Exists(Guid id);
    Task<bool> ExisteIsbnAsync(string isbn, CancellationToken cancellationToken);
    Task<Guid> Add(Livro livro);
    Task InsertAvalicao(Avaliacao avaliacao);
    Task Update(Livro livro);
    Task CommitAsync(CancellationToken cancellationToken = default);




}