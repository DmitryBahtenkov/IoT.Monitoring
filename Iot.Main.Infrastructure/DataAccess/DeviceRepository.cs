using Iot.Main.Domain.Models.DeviceModel;
using Iot.Main.Domain.Services;

namespace Iot.Main.Infrastructure.DataAccess;

public class DeviceRepository : BaseRepository<Device>
{
    public DeviceRepository(IEFContext eFContext, ICurrentUserService currentUserService) : base(eFContext, currentUserService)
    {
    }
}
