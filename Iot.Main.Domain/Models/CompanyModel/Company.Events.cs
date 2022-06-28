using Iot.Main.Domain.Events;

namespace Iot.Main.Domain.Models.CompanyModel;

public partial class Company
{
    public override async Task RaiseCreatedEvent(IEventPublisher<Company> publisher)
    {
        var args = new CreatedEventArgs<Company>(this);
        await publisher.OnCreated(args);
    }

    public override async Task RaiseUpdatedEvent(IEventPublisher<Company> publisher)
    {
        var args = new UpdatedEventArgs<Company>(this);
        await publisher.OnUpdated(args);
    }
}
