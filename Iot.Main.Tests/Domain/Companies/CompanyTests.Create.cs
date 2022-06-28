using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.CompanyModel.DTO;
using Iot.Main.Domain.Shared.Exceptions;
using Xunit;

namespace Iot.Main.Tests.Domain.Companies;

public partial class CompanyTests
{
    [Fact]
    public void CreateValidCompanyTest()
    {
        var request = new CreateCompanyRequest
        {
            Name = "Name",
            Description = "Description"
        };

        var company = Company.Create(request);

        Assert.Equal(request.Name, company.Name);
        Assert.Equal(request.Description, company.Description);
    }

    [Fact]
    public void CreateInvalidCompanyTest()
    {
        var request = new CreateCompanyRequest();

        var exception = Assert.Throws<ValidationException>(() => Company.Create(request));

        Assert.NotEmpty(exception.Result.Errors);
        Assert.Contains(exception.Result.Errors, x => x.Key == nameof(request.Name));
        Assert.Contains(exception.Result.Errors, x => x.Key == nameof(request.Description));
    }
}
