using Iot.Main.Domain.Models.AlertModel;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.ConstraintModel;
using Iot.Main.Domain.Models.ConstraintModel.DTO;
using Iot.Main.Domain.Models.DeviceModel;
using Iot.Main.Domain.Models.InputModel;
using Iot.Main.Domain.Services;
using Iot.Main.Domain.Shared.Exceptions;
using Iot.Main.Domain.Shared.Units;
using Iot.Main.Infrastructure.Commands;

namespace Iot.Main.Infrastructure.Services;

public class ConstraintService : BaseService<Constraint>, IConstraintService
{
    private readonly IRepository<Device> _deviceRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepository<Company> _companyRepository;
    private readonly IRepository<Constraint> _constraintRepository;
    private readonly IRepository<Alert> _alertRepository;
	private readonly SendEmailCommand _sendEmailCommand;

    public ConstraintService(
        IUnitOfWork<Constraint> unitOfWork,
        IRepository<Device> deviceRepository,
        IRepository<Company> companyRepository,
        ICurrentUserService currentUserService,
        IRepository<Alert> alertRepository,
		SendEmailCommand sendEmailCommand) : base(unitOfWork)
    {
			_sendEmailCommand = sendEmailCommand;
        _currentUserService = currentUserService;
        _deviceRepository = deviceRepository;
        _companyRepository = companyRepository;
        _constraintRepository = unitOfWork.GetRepository();
        _alertRepository = alertRepository;
    }

    public async Task<bool> CheckInputData(string token, InputData data)
    {
        var filter = new DeviceFilter
        {
            Token = token
        };

        var device = await _deviceRepository.GetAsync(filter);

        if (device is null)
        {
            throw new BusinessException("Устройство не найдено");
        }

        try
        {
            device.Constraint.CheckInputData(data);
            return true;
        }
        catch (DataAlertException ex)
        {
            var alerts = device.Constraint.Alerts ?? new List<Alert>(0);
            var emails = alerts.Select(x => x.Email).ToArray();
            
            await _sendEmailCommand.SendMany(string.Join(", ", ex.Errors), emails);

            return false;
        }
    }

    public async Task<ConstraintData> Create(CreateConstraintRequest request)
    {
        var company = await GetCompanyOrThrow(_currentUserService.CompanyId!.Value);
        var alerts = await _alertRepository.ListByIdsAsync(request.AlertIds);

        var constraint = Constraint.Create(request, company, alerts);

        constraint = await _constraintRepository.AddAsync(constraint);
        await SaveChanges();

        return constraint.ToData();
    }

    private async Task<Company> GetCompanyOrThrow(int id)
    {
        var filter = new CompanyFilter
        {
            Id = id
        };

        var company = await _companyRepository.GetAsync(filter);

        if (company is null)
        {
            throw new BusinessException("Не найдена компания");
        }

        return company;
    }

    public async Task<ConstraintData?> Get(ConstraintFilter filter)
    {
        var constraint = await _constraintRepository.GetAsync(filter);

        return constraint?.ToData();
    }

    public async Task<List<ConstraintData>> GetAll(ConstraintFilter filter)
    {
        var constraints = await _constraintRepository.ListAsync(filter);

        return constraints.Select(x => x.ToData()).ToList();
    }

    public async Task<ConstraintData> Update(int id, UpdateConstraintRequest request)
    {
        var filter = new ConstraintFilter
        {
            Id = id
        };

        var constraint = await _constraintRepository.GetAsync(filter);

        if (constraint is null)
        {
            throw new BusinessException("Не найдено правило");
        }

        var alerts = await _alertRepository.ListByIdsAsync(request.AlertIds);
        constraint.Update(request, alerts);

        constraint = await _constraintRepository.UpdateAsync(constraint);
        await SaveChanges();

        return constraint.ToData();
    }
}
