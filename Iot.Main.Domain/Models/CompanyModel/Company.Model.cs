using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Iot.Main.Domain.Events;
using Iot.Main.Domain.Extensions;
using Iot.Main.Domain.Models.AlertModel;
using Iot.Main.Domain.Models.ConstraintModel;
using Iot.Main.Domain.Models.DeviceModel;
using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Models.UserModel;
using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Models.CompanyModel;

public partial class Company : EventEntity<Company>
{
    public Company() 
    { }

    public Company(string name, string description)
    {
        Name = name;
        Description = description;

        this.ValidateAndThrow();
    }

    [Required]
    public string Name { get; private set; }
    [Required]
    public string Description { get; private set; }
    [JsonIgnore]
    public virtual List<Role> Roles { get; set; } = new();
    [JsonIgnore]
    public virtual List<User> Users { get; set; } = new();
    [JsonIgnore]
    public virtual List<Device> Devices { get; set; } = new();
    [JsonIgnore]
    public virtual List<Constraint> Constraints { get; set; } = new();
    [JsonIgnore]
    public virtual List<Alert> Alerts { get; set; } = new();
}
