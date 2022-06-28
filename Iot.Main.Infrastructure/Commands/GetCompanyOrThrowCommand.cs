using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Services;
using Iot.Main.Domain.Shared.Exceptions;
using Iot.Main.Domain.Shared.Units;

namespace Iot.Main.Infrastructure.Commands;

public class GetCompanyOrThrowCommand : ICommand<Company>
{
    private readonly IRepository<Company> _repository;
    private readonly ICurrentUserService _currentUserService;

    public GetCompanyOrThrowCommand(
        IRepository<Company> repository,
        ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        _repository = repository;
    }

    public async Task<Company> Execute()
    {
        var filter = new CompanyFilter
        {
            Id = _currentUserService.CompanyId
        };

        var company = await _repository.GetAsync(filter);

        if (company is null)
        {
            throw new BusinessException("Не найдена компания");
        }

        return company;
    }
}
