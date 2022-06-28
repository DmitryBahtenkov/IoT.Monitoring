using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.CompanyModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Domain.Shared.Defaults;
using Iot.Main.Domain.Shared.Exceptions;
using Iot.Main.Domain.Shared.Units;

namespace Iot.Main.Infrastructure.Services;

public class CompanyService : BaseService<Company>, ICompanyService
{
    private readonly IRepository<Company> _repository;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public CompanyService(
        IUnitOfWork<Company> unitOfWork,
        IRoleService roleService,
        IUserService userService) : base(unitOfWork)
    {
        _userService = userService;
        _roleService = roleService;
        _repository = unitOfWork.GetRepository();
    }

    public async Task<CompanyData> Create(CreateCompanyRequest request)
    {
        var nameFilter = new CompanyFilter()
        {
            Name = request.Name,
            IsAccurateSearch = true
        };

        var existedCompany = await _repository.GetAsync(nameFilter);

        if (existedCompany is not null)
        {
            throw new BusinessException("Такая компания уже существует");
        }

        var company = Company.Create(request);
        var role 
            = await _roleService.CreateForCompany(DefaultRoles.AdminRole(company.Name), company);

        var user 
            = await _userService.CreateForCompany(DefaultUsers.CreateUser(company.Name, role.Id), role.Company, role);

        return company.ToData();
    }

    public async Task<CompanyData?> Get(CompanyFilter filter)
    {
        var company = await _repository.GetAsync(filter);

        return company?.ToData();
    }

    public async Task<List<CompanyData>> GetAll(CompanyFilter filter)
    {
        var companies = await _repository.ListAsync(filter);

        return companies.Select(x => x.ToData()).ToList();
    }

    public async Task<CompanyData> Update(int id, UpdateCompanyRequest request)
    {
        var company = await _repository.GetAsync(CompanyFilter.WithId(id));

        if (company is null)
        {
            throw new BusinessException("Компания не найдена");
        }

        company.UpdateInfo(request);

        company = await _repository.UpdateAsync(company);
        await UnitOfWork.SaveChangesAsync();

        return company.ToData();
    }
}
