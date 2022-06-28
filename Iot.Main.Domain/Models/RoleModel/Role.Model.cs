using System.ComponentModel.DataAnnotations;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Models.RoleModel;

public partial class Role : BaseEntity, IEntityWithCompanyId
{
    public Role(string name, Company company)
    {
        Name = name;
        Company = company;
        CompanyId = company.Id;
    }

    public Role()
    { }

    [Required]
    public string Name { get; private set; }
    public int CompanyId { get; private set; }
    public virtual Company Company { get; set; }
}
