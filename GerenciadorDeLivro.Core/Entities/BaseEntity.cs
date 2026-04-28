namespace GerenciadorDeLivro.Core.Entities;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    
    
    
    public void SetDeleted()
    {
        IsDeleted = true;
    }
    
}