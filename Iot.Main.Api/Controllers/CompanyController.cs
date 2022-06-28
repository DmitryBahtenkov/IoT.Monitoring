using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.CompanyModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Iot.Main.Api.Controllers;

/// <summary>
/// Управление компаниями
/// </summary>
[ApiController]
[Route("api/company")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class CompanyController : BaseSwaggerController
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    /// <summary>
    /// Создать компанию
    /// </summary>
    /// <param name="request">Данные о новой компании</param>
    /// <returns>Созданная компания</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Dictionary<string, string>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<CompanyData> Create([FromBody] CreateCompanyRequest request)
        => await _companyService.Create(request);


    /// <summary>
    /// Обновить компанию
    /// </summary>
    /// <param name="id">Id компании</param>
    /// <param name="request">Данные для обновления</param>
    /// <returns>Данные обновлённой компании</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Dictionary<string, string>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<CompanyData> Update(int id, [FromBody] UpdateCompanyRequest request)
        => await _companyService.Update(id, request);

    /// <summary>
    /// Получить одну компанию по фильтру
    /// </summary>
    /// <param name="filter">Фильтр для поиска</param>
    /// <returns>Найденная компания</returns>
    [HttpGet]
    public async Task<CompanyData?> Get([FromQuery] CompanyFilter filter)
        => await _companyService.Get(filter);

    /// <summary>
    /// Получить несколько компаний по фильтру
    /// </summary>
    /// <param name="filter">Фильтр для поиска</param>
    /// <returns>Массив найденных компаний</returns>
    [HttpGet("many")]
    public async Task<List<CompanyData>> Many([FromQuery] CompanyFilter filter)
        => await _companyService.GetAll(filter);

    [HttpDelete("{id}")]
    public async Task<Company> Delete(int id)
        => await ((BaseService<Company>)_companyService).Archive(id);
}

