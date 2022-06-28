using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Iot.Main.Domain.Models.AlertModel;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.DeviceModel;
using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Models.ConstraintModel;

public partial class Constraint : BaseEntity, IEntityWithCompanyId
{
    public double? MinTemp { get; private set; }
    public double? MinHumidity { get; private set; }
    public double? MinPressure { get; private set; }
    public double? MaxTemp { get; private set; }
    public double? MaxHumidity { get; private set; }
    public double? MaxPressure { get; private set; }
    [Required]
    public string Name { get; private set; }

    public Constraint()
    { }

    public Constraint(
        string name,
        Company company,
        double? minTemp = null, 
        double? minHumidity = null,
        double? minPressure = null,
        double? maxTemp = null, 
        double? maxHumidity = null, 
        double? maxPressure = null,
        List<Alert> alerts = null)
    {
        Name = name;
        Company = company;
        MinTemp = minTemp;
        MinHumidity = minHumidity;
        MinPressure = minPressure;
        MaxTemp = maxTemp;
        MaxHumidity = maxHumidity;
        MaxPressure = maxPressure;
        Alerts = alerts ?? new List<Alert>();
    }

    [Required]
    public int CompanyId { get; private set; }
    public virtual Company Company { get; set; }
    [JsonIgnore]
    public virtual List<Device> Devices { get; set; } = new();
    [JsonIgnore]
    public virtual List<Alert> Alerts { get; set; } = new();
}
