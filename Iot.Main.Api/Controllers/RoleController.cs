using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Models.RoleModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iot.Main.Api.Controllers;

/// <summary>
/// Управление ролями
/// </summary>
[ApiController]
[Route("api/role")]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class RoleController : BaseSwaggerController
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// Создать новую роль
    /// </summary>
    /// <param name="request">Данные для новой роли</param>
    /// <returns>Созданная роль</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Dictionary<string, string>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<Role> Create([FromBody] CreateRoleRequest request)
        => await _roleService.Create(request);

    /// <summary>
    /// Обновить роль
    /// </summary>
    /// <param name="id">Id роли</param>
    /// <param name="request">Данные для обновления</param>
    /// <returns>Обновлённая роль</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Dictionary<string, string>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<Role> Update(int id, [FromBody] UpdateRoleRequest request)
        => await _roleService.Update(id, request);

    /// <summary>
    /// Получить одну роль
    /// </summary>
    /// <param name="filter">Фильтр для поиска</param>
    /// <returns>Найденная роль</returns>
    [HttpGet]
    public async Task<Role?> Get([FromQuery] RoleFilter filter)
        => await _roleService.Get(filter);

    /// <summary>
    /// Получить несколько ролей по фильтру
    /// </summary>
    /// <param name="filter">Фильтр для поиска</param>
    /// <returns>Найденные роли</returns>
    [HttpGet("many")]
    public async Task<List<Role>> Many([FromQuery] RoleFilter filter)
        => await _roleService.GetAll(filter);

    [HttpDelete("{id}")]
    public async Task<Role> Delete(int id)
        => await ((BaseService<Role>)_roleService).Archive(id);
}