using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Models.RoleModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Domain.Shared.Exceptions;
using Iot.Main.Domain.Shared.Units;
using Iot.Main.Infrastructure.Commands;

namespace Iot.Main.Infrastructure.Services;

public class RoleService : BaseService<Role>, IRoleService
{
	private readonly GetCompanyOrThrowCommand _getCompanyOrThrowCommand;
    private readonly IRepository<Role> _roleRepository;

    public RoleService(IUnitOfWork<Role> unitOfWork, GetCompanyOrThrowCommand getCompanyOrThrowCommand) : base(unitOfWork)
    {
        _getCompanyOrThrowCommand = getCompanyOrThrowCommand;
        _roleRepository = unitOfWork.GetRepository();
    }

    public async Task<Role> Create(CreateRoleRequest request)
    {
        var company = await _getCompanyOrThrowCommand.Execute();
        var role = Role.Create(request, company);

        role = await _roleRepository.AddAsync(role);
        await SaveChanges();

        return role;
    }

    public async Task<Role?> Get(RoleFilter filter)
    {
        return await _roleRepository.GetAsync(filter);
    }

    public async Task<List<Role>> GetAll(RoleFilter filter)
    {
        return await _roleRepository.ListAsync(filter);
    }

    public async Task<Role> Update(int id, UpdateRoleRequest request)
    {
        var role = await _roleRepository.BuIdAsync(id);
        if (role is null)
        {
            throw new BusinessException("Не найдена роль");
        }

        role.Update(request);

        role = await _roleRepository.AddAsync(role);
        await SaveChanges();

        return role;
    }

    public async Task<Role> CreateForCompany(CreateRoleRequest request, Company company)
    {
        var role = Role.Create(request, company);

        role = await _roleRepository.AddAsync(role);
        await SaveChanges();

        return role;
    }
}
