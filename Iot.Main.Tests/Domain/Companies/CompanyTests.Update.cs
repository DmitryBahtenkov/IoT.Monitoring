using Iot.Main.Domain.Models.CompanyModel.DTO;
using Iot.Main.Domain.Shared.Exceptions;
using Xunit;

namespace Iot.Main.Tests.Domain.Companies;

public partial class CompanyTests
{
    [Fact]
    public void UpdateValidCompanyTest()
    {
        var request = new UpdateCompanyRequest
        {
            Name = "newname",
            Description = "newdesc"
        };

        var company = TestCompanies.CompanyWithRole;

        company.UpdateInfo(request);

        Assert.Equal(request.Name, company.Name);
        Assert.Equal(request.Description, company.Description);
    }

    [Fact]
    public void UpdateInvalidCompanyTest()
    {
        var request = new UpdateCompanyRequest();

        var company = TestCompanies.CompanyWithRole;

        var exception = Assert.Throws<ValidationException>(() => company.UpdateInfo(request));
        Assert.NotEmpty(exception.Result.Errors);
        Assert.Contains(exception.Result.Errors, x => x.Key == nameof(request.Name));
        Assert.Contains(exception.Result.Errors, x => x.Key == nameof(request.Description));
    }
}
