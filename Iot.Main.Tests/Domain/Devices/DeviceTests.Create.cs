using Iot.Main.Domain.Models.DeviceModel;
using Iot.Main.Domain.Models.DeviceModel.DTO;
using Xunit;
using Iot.Main.Tests.Domain.Constraints;
using Iot.Main.Tests.Domain.Companies;
using Iot.Main.Domain.Shared.Exceptions;

namespace Iot.Main.Tests.Domain.Devices;

public partial class DeviceTests
{
    [Fact]
    public void CreateValidDeviceTest()
    {
        var request = new CreateDeviceRequest
        {
            Name = "name",
            Token = "tokentoken",
            ConstraintId = 1
        };

        var device = Device.Create(request, TestCompanies.CompanyWithDevice);

        Assert.Equal(request.Name, device.Name);
        Assert.Equal(request.Token, device.Token);
    }

    [Fact]
    public void CreateInvalidDeviceTest()
    {
        var request = new CreateDeviceRequest();

        var ex = Assert.Throws<ValidationException>(() => Device.Create(request, TestCompanies.CompanyWithRole));

        Assert.NotEmpty(ex.Result.Errors);
        Assert.Contains(ex.Result.Errors, x => x.Key == nameof(request.Name));
        Assert.Contains(ex.Result.Errors, x => x.Key == nameof(request.Token));
    }

    [Fact]
    public void CreateDuplicateDeviceTest()
    {
        var request = new CreateDeviceRequest
        {
            Name = "name",
            Token = "token",
            ConstraintId = 1
        };

        Assert.Throws<BusinessException>(() => Device.Create(request, TestCompanies.CompanyWithDevice));
    }
}
