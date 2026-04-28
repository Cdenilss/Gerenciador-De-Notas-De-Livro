using GerenciadorDeLivro.Core.Entities;

namespace GerenciadorDeLivro.Core.Repository;

public interface IUsuarioRepository
{

    Task<List<Usuario>> GetAll();
    Task<Usuario?> GetDetailsById(Guid id);
    Task<Usuario?> GetById(Guid id);
    Task<Guid>Add(Usuario usuario);
    Task<Guid> Update(Usuario usuario);
    Task<bool> Exist(Guid id);
    
}