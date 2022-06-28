using Iot.Main.Domain.Models.DeviceModel;
using Iot.Main.Domain.Models.DeviceModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iot.Main.Api.Controllers;

/// <summary>
/// Управление устройствами
/// </summary>
[ApiController]
[Route("api/device")]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class DeviceController : BaseSwaggerController
{
    private readonly IDeviceService _deviceService;

    public DeviceController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    /// <summary>
    /// Добавить новое устройство
    /// </summary>
    /// <param name="request">Данные о новом устройстве</param>
    /// <returns>Созданное устройство</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Dictionary<string, string>), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(DeviceData), StatusCodes.Status200OK)]
    public async Task<DeviceData> Create([FromBody] CreateDeviceRequest request)
        => await _deviceService.Create(request);

    /// <summary>
    /// Получить одно устройство по фильтру
    /// </summary>
    /// <param name="filter">Фильтр для поиска</param>
    /// <returns>Данные об устройстве</returns>
    [HttpGet]
    public async Task<DeviceData?> Get([FromQuery] DeviceFilter filter)
        => await _deviceService.Get(filter);

    /// <summary>
    /// Получить несколько устройств
    /// </summary>
    /// <param name="filter">Фильтр для поиска</param>
    /// <returns>Массив найденных устройств</returns>
    [HttpGet("many")]
    public async Task<List<DeviceData>> Many([FromQuery] DeviceFilter filter)
        => await _deviceService.GetAll(filter);

    [HttpDelete("{id}")]
    public async Task<Device> Delete(int id)
        => await ((BaseService<Device>)_deviceService).Archive(id);
}