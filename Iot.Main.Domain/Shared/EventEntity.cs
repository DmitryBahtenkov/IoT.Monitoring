using Iot.Main.Domain.Events;

namespace Iot.Main.Domain.Shared;

public abstract class EventEntity<TEntity> : BaseEntity  where TEntity : IEntityModel
{
    public abstract Task RaiseCreatedEvent(IEventPublisher<TEntity> publisher);
    public abstract Task RaiseUpdatedEvent(IEventPublisher<TEntity> publisher);
}
