using System.ComponentModel.DataAnnotations;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.ConstraintModel;
using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Models.DeviceModel;

public partial class Device : BaseEntity, IEntityWithCompanyId
{
    [Required]
    public string Token { get; private set; }

    public Device()
    { }

    public Device(string token, string name, Constraint constraint, Company company)
    {
        Token = token;
        Name = name;
        Constraint = constraint;
        Company = company;
        IsEnabled = false;
    }

    public string Name { get; private set; }
    public bool IsEnabled { get; private set; }
    public int ConstraintId { get; private set; }
    public virtual Constraint Constraint { get; set; }
    public int CompanyId { get; private set; }
    public virtual Company Company { get; set; }
}
