using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfluxDB.Client.Core.Exceptions;
using Iot.Main.Domain.Models.DeviceModel;
using Iot.Main.Domain.Models.InputModel;
using Iot.Main.Domain.Shared.Units;
using Iot.Main.Infrastructure.Builders;
using Iot.Main.Infrastructure.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Iot.Main.Infrastructure.DataRecive;

namespace Iot.Main.Api.Controllers;

[ApiController]
[Route("api/data")]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]

public class DataController : ControllerBase
{
    private readonly GetInfluxDataCommand _getInfluxData;

    public DataController(GetInfluxDataCommand getInfluxData)
    {
        _getInfluxData = getInfluxData;
    }

    /// <summary>
    /// Получить данные из Influx
    /// </summary>
    /// <param name="deviceId">ID устройства</param>
    /// <returns>Данные из Influx: Humidity, Pressure, Temp</returns>
    [HttpGet("{deviceId}")]
    public async Task<InputData[]> Get(int deviceId) =>
        await _getInfluxData.Get(deviceId);

}
