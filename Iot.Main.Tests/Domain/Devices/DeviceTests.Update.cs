using Iot.Main.Domain.Models.ConstraintModel;
using Iot.Main.Domain.Shared.Exceptions;
using Xunit;

namespace Iot.Main.Tests.Domain.Devices;

public partial class DeviceTests
{
    [Fact]
    public void SetValidConstraintTest()
    {
        var device = TestDevices.Device;

        device.SetConstraint(new Constraint() { Id = 1 });

        Assert.Equal(1, device.ConstraintId);
    }

    [Fact]
    public void SetNotExistedConstraintTest()
    {
        var device = TestDevices.Device;

        Assert.Throws<BusinessException>(() => device.SetConstraint(new Constraint() { Id = 11 }));
    }

    [InlineData(true)]
    [InlineData(false)]
    [Theory]
    public void SetStateTest(bool state)
    {
        var device = TestDevices.Device;

        device.SetState(state);

        Assert.Equal(state, device.IsEnabled);
    }
}
