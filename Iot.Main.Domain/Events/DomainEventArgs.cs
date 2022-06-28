using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Events;

public abstract class DomainEventArgs<TEntity> where TEntity : IEntityModel
{
    public string EventId { get; }
    public DateTime CreatedAt { get; }
    public TEntity Entity { get; }

    public DomainEventArgs(TEntity entity)
    {
        Entity = entity;
        EventId = Guid.NewGuid().ToString("N");
        CreatedAt = DateTime.UtcNow;
    }
}
