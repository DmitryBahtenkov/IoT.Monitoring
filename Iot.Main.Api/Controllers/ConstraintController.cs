using Iot.Main.Domain.Models.ConstraintModel;
using Iot.Main.Domain.Models.ConstraintModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iot.Main.Api.Controllers;

/// <summary>
/// Управление ограничениями для данных
/// </summary>
[ApiController]
[Route("api/constraint")]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class ConstraintController : BaseSwaggerController
{
    private readonly IConstraintService _constraintService;

    public ConstraintController(IConstraintService constraintService)
    {
        _constraintService = constraintService;
    }

    /// <summary>
    /// Создать новое ограничение
    /// </summary>
    /// <param name="request">Данные для нового ограничения</param>
    /// <returns>Данные о созданном ограничении</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Dictionary<string, string>), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ConstraintData), StatusCodes.Status200OK)]
    public async Task<ConstraintData> Create([FromBody] CreateConstraintRequest request)
        => await _constraintService.Create(request);

    /// <summary>
    /// Обновить ограничение
    /// </summary>
    /// <param name="id">Id ограничения</param>
    /// <param name="request">Данные для обновления</param>
    /// <returns>Обновлённое ограничение</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Dictionary<string, string>), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ConstraintData), StatusCodes.Status200OK)]
    public async Task<ConstraintData> Update(int id, [FromBody] UpdateConstraintRequest request)
        => await _constraintService.Update(id, request);

    /// <summary>
    /// Получить одно ограничение по фильтру
    /// </summary>
    /// <param name="filter">Фильтр для поиска</param>
    /// <returns>Найденное ограничение</returns>
    [HttpGet]
    public async Task<ConstraintData?> Get([FromQuery] ConstraintFilter filter)
        => await _constraintService.Get(filter);


    /// <summary>
    /// Получить несколько ограничение по фильтру
    /// </summary>
    /// <param name="filter">Фильтр для поиска</param>
    /// <returns>Массив найденных ограничений</returns>
    [HttpGet("many")]
    public async Task<List<ConstraintData>> Many([FromQuery] ConstraintFilter filter)
        => await _constraintService.GetAll(filter);

    [HttpDelete("{id}")]
    public async Task<Constraint> Delete(int id)
        => await ((BaseService<Constraint>)_constraintService).Archive(id);
}
