using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.ConstraintModel;
using Iot.Main.Domain.Models.DeviceModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Infrastructure.DataAccess;
using Iot.Main.Tests.Infrastructure.Base;
using Xunit;
using Xunit.Abstractions;

namespace Iot.Main.Tests.Infrastructure.Devices;

public class DeviceServiceTests : BaseTestCase
{
    private readonly IDeviceService _deviceService;
    private Constraint _constraint;
    private DeviceData _deviceData;
    private CreateDeviceRequest _request;

    public DeviceServiceTests(IEFContext eFContext, ICurrentUserService currentUserService, ITestOutputHelper outputHelper, IDeviceService deviceService) : base(eFContext, currentUserService, outputHelper)
    {
        _deviceService = deviceService;
    }

    [Fact]
    public async Task CreateDeviceTest()
    {
        await RunSteps(
            Given_the_company,
            Given_the_admin_role,
            Given_the_authenticated_user,
            Given_the_constraint,
            Given_the_create_request,
            When_device_created,
            Then_device_is_saved
        );
    }

    private async Task Given_the_constraint()
    {
        _constraint = new Constraint("test", Company)
        {
            Id = 1
        };

        Company.Constraints.Add(_constraint);

        await _eFContext.Set<Constraint>().AddAsync(_constraint);
        await _eFContext.SaveChangesAsync();
    }
    
    private Task Given_the_create_request()
    {
        _request = new CreateDeviceRequest
        {
            Name = "test",
            ConstraintId = _constraint.Id,
            Token = "12345"
        };

        return Task.CompletedTask;
    }

    private async Task When_device_created()
    {
        _deviceData = await _deviceService.Create(_request);
    }

    private Task Then_device_is_saved()
    {
        Assert.NotNull(_deviceData);   
        Assert.Equal(_request.Name, _deviceData.Name);   
        Assert.False(_deviceData.State); 

        return Task.CompletedTask;  
    }
}
