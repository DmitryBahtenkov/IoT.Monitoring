using Iot.Main.Domain.Models.ConstraintModel;
using Iot.Main.Domain.Models.ConstraintModel.DTO;
using Iot.Main.Domain.Shared.Exceptions;
using Iot.Main.Tests.Domain.Companies;
using Xunit;

namespace Iot.Main.Tests.Domain.Constraints;

public partial class ConstraintTests
{
    [Fact]
    public void CreateValidConstraintTest()
    {
        var request = new CreateConstraintRequest()
        {
            Name = "test",
            MaxHumidity = 1,
            MaxPressure = 2,
            MaxTemp = 22,
            MinHumidity = -1,
            MinPressure = -22,
            MinTemp = -222
        };

        var constraint = Constraint.Create(request, TestCompanies.CompanyWithDevice);

        Assert.Equal(request.Name, constraint.Name);
        Assert.Equal(request.MaxHumidity, constraint.MaxHumidity);
        Assert.Equal(request.MaxPressure, constraint.MaxPressure);
        Assert.Equal(request.MaxTemp, constraint.MaxTemp);
        Assert.Equal(request.MinHumidity, constraint.MinHumidity);
        Assert.Equal(request.MinTemp, constraint.MinTemp);
        Assert.Equal(request.MinPressure, constraint.MinPressure);
    }

    [Fact]
    public void CreateInvalidConstraintTest()
    {
        var request = new CreateConstraintRequest();

        var ex = Assert.Throws<ValidationException>(() => Constraint.Create(request, TestCompanies.CompanyWithDevice));

        Assert.NotEmpty(ex.Result.Errors);
        Assert.Contains(ex.Result.Errors, x => x.Key == nameof(request.Name));
    }

    [Fact]
    public void CreateEmptyConstraintTest()
    {
        var request = new CreateConstraintRequest()
        {
            Name = "test"
        };

        var constraint = Constraint.Create(request, TestCompanies.CompanyWithDevice);

        Assert.Equal(request.Name, constraint.Name);
        Assert.Equal(request.MaxHumidity, null);
        Assert.Equal(request.MaxPressure, null);
        Assert.Equal(request.MaxTemp, null);
        Assert.Equal(request.MinHumidity, null);
        Assert.Equal(request.MinTemp, null);
        Assert.Equal(request.MinPressure, null);
    }

    [Fact]
    public void CreateDuplicateConstraintTest()
    {
        var request = new CreateConstraintRequest()
        {
            Name = "cnam"
        };

        Assert.Throws<BusinessException>(() => Constraint.Create(request, TestCompanies.CompanyWithDevice));
    }
}
