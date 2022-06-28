using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.ConstraintModel.DTO;
using Iot.Main.Domain.Shared.Exceptions;
using Xunit;

namespace Iot.Main.Tests.Domain.Constraints;

public partial class ConstraintTests
{
    [Fact]
    public void UpdateValidConsrtaintTest()
    {
        var request = new UpdateConstraintRequest
        {
            Name = "test",
            MaxHumidity = 1,
            MaxPressure = 2,
            MaxTemp = 22,
            MinHumidity = -1,
            MinPressure = -22,
            MinTemp = -222
        };

        var constraint = TestConstraints.ConstraintWithCompany;

        constraint.Update(request);

        Assert.Equal(request.Name, constraint.Name);
        Assert.Equal(request.MaxHumidity, constraint.MaxHumidity);
        Assert.Equal(request.MaxPressure, constraint.MaxPressure);
        Assert.Equal(request.MaxTemp, constraint.MaxTemp);
        Assert.Equal(request.MinHumidity, constraint.MinHumidity);
        Assert.Equal(request.MinTemp, constraint.MinTemp);
        Assert.Equal(request.MinPressure, constraint.MinPressure);
    }

    [Fact]
    public void UpdateInvalidConstraintTest()
    {
        var request = new UpdateConstraintRequest();
        var constraint = TestConstraints.Constraint;

        var ex = Assert.Throws<ValidationException>(() => constraint.Update(request));

        Assert.NotEmpty(ex.Result.Errors);
        Assert.Contains(ex.Result.Errors, x => x.Key == nameof(request.Name));
    }

    [Fact]
    public void UpdateDuplicateConstraintTest()
    {
        var request = new UpdateConstraintRequest()
        {
            Name = "cnam"
        };

        var constraint = TestConstraints.ConstraintWithCompany;


        Assert.Throws<BusinessException>(() => constraint.Update(request));
    }
}
