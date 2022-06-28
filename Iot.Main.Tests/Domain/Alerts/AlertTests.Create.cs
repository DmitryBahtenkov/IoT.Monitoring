using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.AlertModel;
using Iot.Main.Domain.Models.AlertModel.DTO;
using Iot.Main.Domain.Shared.Exceptions;
using Iot.Main.Tests.Domain.Companies;
using Xunit;

namespace Iot.Main.Tests.Domain.Alerts;

public partial class AlertTests
{
    [Fact]
    public void CreateValidAlertTest()
    {
        var request = new AlertRequest
        {
            Name = "test",
            Email = "t@t.t"
        };

        var alert = Alert.Create(request, TestCompanies.CompanyWithDevice);

        Assert.Equal(request.Name, alert.Name);
        Assert.Equal(request.Email, alert.Email);
    }

    [Fact]
    public void CreateDuplicateAlertTest()
    {
        var request = new AlertRequest
        {
            Name = "test",
            Email = TestAlerts.Alert.Email
        };

        var ex = Assert.Throws<BusinessException>(() => Alert.Create(request, TestCompanies.CompanyWithDevice));
        Assert.Contains(TestAlerts.Alert.Name, ex.Message);
    }

    [Fact]
    public void CreateInvalidAlertTest()
    {
        var request = new AlertRequest();

        var ex = Assert.Throws<ValidationException>(() => Alert.Create(request, TestCompanies.CompanyWithDevice));

        Assert.Contains(ex.Result.Errors, x => x.Key == nameof(AlertRequest.Name));
        Assert.Contains(ex.Result.Errors, x => x.Key == nameof(AlertRequest.Email));
    }

}
