using GerenciadorDeLivro.Core.Entities;

namespace GerenciadorDeLivro.Core.Repository;

public interface IAvaliacaoRepository
{
    Task<List<Avaliacao>> GetAll();
    Task<Avaliacao?> GetById(Guid id);
    Task <Avaliacao?> GetDetailsById(Guid id);
    Task<bool> ExistsAvaliacaoByUserId(Guid userId, Guid livroId); 
}