using Myrtex.Domain.Interfaces;

namespace Myrtex.Domain;

public abstract class Entity : IEntity
{
    public int Id { get; set; }
}
