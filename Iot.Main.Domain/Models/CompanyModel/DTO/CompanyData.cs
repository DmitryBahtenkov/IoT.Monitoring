using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Models.CompanyModel.DTO;

public class CompanyData : BaseData
{
    public string Name { get; set; }
    public string Description { get; set; }
}
