using Iot.Main.Domain.Models.AlertModel;
using Iot.Main.Domain.Models.AlertModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Domain.Shared.Exceptions;
using Iot.Main.Domain.Shared.Units;
using Iot.Main.Infrastructure.Commands;

namespace Iot.Main.Infrastructure.Services;

public class AlertService : BaseService<Alert>, IAlertService
{
    private readonly IRepository<Alert> _alertRepository;
    private readonly GetCompanyOrThrowCommand _getCompanyOrThrowCommand;

    public AlertService(
        IUnitOfWork<Alert> unitOfWork,
        GetCompanyOrThrowCommand getCompanyOrThrowCommand) : base(unitOfWork)
    {
        _getCompanyOrThrowCommand = getCompanyOrThrowCommand;
        _alertRepository = unitOfWork.GetRepository();
    }

    public async Task<Alert> Create(AlertRequest request)
    {
        var company = await _getCompanyOrThrowCommand.Execute();

        var alert = Alert.Create(request, company);

        alert = await _alertRepository.AddAsync(alert);
        await SaveChanges();

        return alert;
    }

    public async Task<Alert?> Get(AlertFilter filter)
    {
        return await _alertRepository.GetAsync(filter);
    }

    public async Task<List<Alert>> GetAll(AlertFilter filter)
    {
        return await _alertRepository.ListAsync(filter);
    }

    public async Task<Alert> Update(int id, AlertRequest request)
    {
        var alert = await _alertRepository.BuIdAsync(id);
        if (alert is null)
        {
            throw new BusinessException("Оповещение не найдено");
        }

        alert.Update(request);
        alert = await _alertRepository.UpdateAsync(alert);
        await SaveChanges();

        return alert;
    }
}