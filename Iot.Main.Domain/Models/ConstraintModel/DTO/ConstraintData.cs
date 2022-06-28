using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Models.ConstraintModel.DTO;

public class ConstraintData : BaseData
{
    public double? MinTemp { get; set; }
    public double? MinHumidity { get; set; }
    public double? MinPressure { get; set; }
    public double? MaxTemp { get; set; }
    public double? MaxHumidity { get; set; }
    public double? MaxPressure { get; set; }
    public string Name { get; set; }
}
