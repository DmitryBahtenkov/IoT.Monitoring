using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.DeviceModel;
using Iot.Main.Domain.Models.DeviceModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Domain.Shared.Exceptions;
using Iot.Main.Domain.Shared.Units;

namespace Iot.Main.Infrastructure.Services;

public class DeviceService : BaseService<Device>, IDeviceService
{
    private readonly IRepository<Company> _companyRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepository<Device> _deviceRepository;

    public DeviceService(
        IUnitOfWork<Device> unitOfWork,
        IRepository<Company> companyRepository,
        ICurrentUserService currentUserService) : base(unitOfWork)
    {
        _currentUserService = currentUserService;
        _companyRepository = companyRepository;
        _deviceRepository = unitOfWork.GetRepository();
    }

    public async Task<DeviceData> Create(CreateDeviceRequest request)
    {
        var company = await GetCompanyOrThrow(_currentUserService.CompanyId!.Value);

        var device = Device.Create(request, company);
        await _deviceRepository.AddAsync(device);
        await SaveChanges();

        return device.ToData();
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

    public async Task<DeviceData?> Get(DeviceFilter filter)
    {
        var device = await _deviceRepository.GetAsync(filter);

        return device?.ToData();
    }

    public async Task<List<DeviceData>> GetAll(DeviceFilter filter)
    {
        var devices = await _deviceRepository.ListAsync(filter);

        return devices
            .Select(x => x.ToData())
            .ToList();
    }
}
