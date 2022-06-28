using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Models.DeviceModel.DTO;

public class DeviceData : BaseData
{
    public DeviceData(int id, string name, bool state, bool isArchive)
    {
        Id = id;
        Name = name;
        State = state;
        IsArchive = isArchive;
    }
    
    public string Name { get; set; }
    public bool State { get; set; }
}
