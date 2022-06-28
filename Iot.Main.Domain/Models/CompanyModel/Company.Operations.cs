using Iot.Main.Domain.Extensions;
using Iot.Main.Domain.Models.CompanyModel.DTO;

namespace Iot.Main.Domain.Models.CompanyModel;

public partial class Company
{
    public static Company Create(CreateCompanyRequest request)
    {
        request.ValidateAndThrow();

        return new Company(request.Name, request.Description);
    }

    public void UpdateInfo(UpdateCompanyRequest request)
    {
        request.ValidateAndThrow();

        Name = request.Name;
        Description = request.Description;
    }

    public CompanyData ToData()
    {
        return new CompanyData
        {
            Id = Id,
            Name = Name,
            Description = Description
        };
    }
}
