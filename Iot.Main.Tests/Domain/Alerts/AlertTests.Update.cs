using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.AlertModel.DTO;
using Iot.Main.Domain.Shared.Exceptions;
using Xunit;

namespace Iot.Main.Tests.Domain.Alerts;

public partial class AlertTests
{
    [Fact]
    public void UpdateValidAlert()
    {
        var request = new AlertRequest
        {
            Name = "new",
            Email = "new@n.n"
        };

        var alert = TestAlerts.Alert;

        alert.Update(request);

        Assert.Equal(request.Email, alert.Email);
        Assert.Equal(request.Name, alert.Name);
    }

    [Fact]
    public void UpdateInvalidAlertTest()
    {
        var request = new AlertRequest();

        var alert = TestAlerts.Alert;
        var ex = Assert.Throws<ValidationException>(() => alert.Update(request));

        Assert.Contains(ex.Result.Errors, x => x.Key == nameof(AlertRequest.Name));
        Assert.Contains(ex.Result.Errors, x => x.Key == nameof(AlertRequest.Email));
    }
}
