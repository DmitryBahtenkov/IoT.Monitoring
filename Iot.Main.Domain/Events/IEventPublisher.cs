using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Events;

public interface IEventPublisher<TEntity> where TEntity : IEntityModel
{
    public Task OnCreated(CreatedEventArgs<TEntity> eventArgs);
    public Task OnUpdated(UpdatedEventArgs<TEntity> eventArgs);
}
