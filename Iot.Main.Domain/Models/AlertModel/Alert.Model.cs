using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.ConstraintModel;
using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Models.AlertModel;

public partial class Alert : BaseEntity, IEntityWithCompanyId
{
    public string Email { get; private set; }
    public string Name { get; private set; }

    public Alert()
    {}

    public Alert(string email, string name, Company company)
    {
        Email = email;
        Company = company;
        CompanyId = company.Id;
        Name = name;
    }

    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }
    [JsonIgnore]
    public virtual List<Constraint> Constraints { get; set; } = new();
}
