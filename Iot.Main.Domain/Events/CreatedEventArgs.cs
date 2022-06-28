using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Events;

public class CreatedEventArgs<TEntity> : DomainEventArgs<TEntity> where TEntity : IEntityModel
{
    public CreatedEventArgs(TEntity entity) : base(entity)
    {
    }
}
