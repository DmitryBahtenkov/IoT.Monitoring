using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Models.UserModel;
using Iot.Main.Domain.Models.UserModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Domain.Shared;
using Iot.Main.Domain.Shared.Exceptions;
using Iot.Main.Domain.Shared.Units;

namespace Iot.Main.Infrastructure.Services;

public class UserService : BaseService<User>, IUserService
{
    private readonly IRepository<User> _repository;
    private readonly IRepository<Company> _companyRepository;
    private readonly ICurrentUserService _currentUserService;

    public UserService(
        IUnitOfWork<User> unitOfWork,
        ICurrentUserService currentUserService,
        IRepository<Company> companyRepository) : base(unitOfWork)
    {
        _companyRepository = companyRepository;
        _currentUserService = currentUserService;
        _repository = unitOfWork.GetRepository();
    }

    public async Task<UserData> Create(CreateUserRequest request)
    {
        var company = await GetCompanyOrThrow(_currentUserService.CompanyId!.Value);

        var role = GetRoleOrThrow(company, request.RoleId);

        var user = User.Create(request, role, company);

        user = await _repository.AddAsync(user);
        await SaveChanges();

        return user.ToData();
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

    private Role GetRoleOrThrow(Company company, int id)
    {
        return company.Roles.Find(x => x.Id == id) ?? throw new BusinessException("Не найдена роль");
    }

    public async Task<UserData?> Get(UserFilter filter)
    {
        var user = await _repository.GetAsync(filter);
        
        return user?.ToData();
    }

    public async Task<List<UserData>> GetAll(UserFilter filter)
    {
        var users = await _repository.ListAsync(filter);
        
        return users
            .Select(x => x.ToData())
            .ToList();
    }

    public async Task<UserData> Update(int id, UpdateUserRequest request)
    {
        var filter = new UserFilter
        {
            Id = id
        };
        var user = await _repository.GetAsync(filter);

        if (user is null)
        {
            throw new BusinessException("Не найден пользователь");
        }

        user.UpdateInfo(request);
        user = await _repository.UpdateAsync(user);
        await SaveChanges();

        return user.ToData();
    }

    public async Task<UserData> CreateForCompany(CreateUserRequest request, Company company, Role role)
    {
        var user = User.Create(request, role, company);
        user = await _repository.AddAsync(user);
        await SaveChanges();

        return user.ToData(); 
    }
}
