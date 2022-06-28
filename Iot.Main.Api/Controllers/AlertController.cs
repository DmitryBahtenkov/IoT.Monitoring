using Iot.Main.Domain.Models.AlertModel;
using Iot.Main.Domain.Models.AlertModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iot.Main.Api.Controllers;

/// <summary>
/// Управление настройками оповещений
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class AlertController : BaseSwaggerController
{
    private readonly IAlertService _alertService;

    public AlertController(IAlertService alertService)
    {
        _alertService = alertService;
    }

    /// <summary>
    /// Создать настройку оповещения
    /// </summary>
    /// <param name="request">Данные для создания</param>
    /// <returns>Созданная модель</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Dictionary<string, string>), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Alert), StatusCodes.Status200OK)]
    public async Task<Alert> Create([FromBody] AlertRequest request)
        => await _alertService.Create(request);


    /// <summary>
    /// Обновить настройку оповещения
    /// </summary>
    /// <param name="id">Id настройки</param>
    /// <param name="request">Данные для обновления</param>
    /// <returns>Обновлённая модель</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Dictionary<string, string>), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Alert), StatusCodes.Status200OK)]
    public async Task<Alert> Update(int id, [FromBody] AlertRequest request)
        => await _alertService.Update(id, request);

    /// <summary>
    /// Получить одну настройку по фильтру
    /// </summary>
    /// <param name="filter">Фильтр для поиска</param>
    /// <returns>Найденный объект</returns>
    [HttpGet]
    public async Task<Alert?> Get([FromQuery] AlertFilter filter)
        => await _alertService.Get(filter);

    /// <summary>
    /// Получить массив настроек оповещений по фильтру
    /// </summary>
    /// <param name="filter">Фильтр для поиска</param>
    /// <returns>Найденный объект</returns>
    [HttpGet("many")]
    public async Task<List<Alert>> Many([FromQuery] AlertFilter filter)
        => await _alertService.GetAll(filter);

    [HttpDelete("{id}")]
    public async Task<Alert> Delete(int id)
        => await ((BaseService<Alert>)_alertService).Archive(id);
}
