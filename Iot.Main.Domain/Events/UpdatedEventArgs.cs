using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Events
{
    public class UpdatedEventArgs<TEntity> : DomainEventArgs<TEntity> where TEntity : IEntityModel
    {
        public UpdatedEventArgs(TEntity entity) : base(entity)
        {
        }
    }
}