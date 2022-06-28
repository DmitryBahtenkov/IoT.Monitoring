using System;
using System.Collections.Generic;
#nullable enable
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.DeviceModel;
using Iot.Main.Domain.Models.DeviceModel.DTO;

namespace Iot.Main.Domain.Services;

public interface IDeviceService
{
    public Task<DeviceData> Create(CreateDeviceRequest request);
    public Task<DeviceData?> Get(DeviceFilter filter);
    public Task<List<DeviceData>> GetAll(DeviceFilter filter);
}
