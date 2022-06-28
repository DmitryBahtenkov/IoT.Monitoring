using Iot.Main.Domain.Aggregates;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.UserModel;
using Iot.Main.Domain.Models.UserModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Domain.Shared.Defaults;
using Iot.Main.Domain.Shared.Exceptions;
using Iot.Main.Domain.Shared.Units;

namespace Iot.Main.Infrastructure.Services;

public class AuthService : BaseService<User>, IAuthService
{
    private readonly IRepository<User> _repository;
    private readonly IUserService _userService;
    private readonly ICompanyService _companyService;
	private readonly IRepository<Company> _companyRepository;
    private readonly IRoleService _roleService;

    public AuthService(
        IUnitOfWork<User> unitOfWork,
        ICompanyService companyService,
        IUserService userService,
        IRoleService roleService,
		IRepository<Company> companyRepository) : base(unitOfWork)
    {
		_companyRepository = companyRepository;
        _roleService = roleService;
        _userService = userService;
        _companyService = companyService;
        _repository = unitOfWork.GetRepository();
    }

    public async Task<UserData> Login(LoginRequest request)
    {
        var filter = new UserFilter
        {
            Login = request.Login,
            IsAccurateLoginSearch = true
        };

        var user = await GetUserOrThrow(filter);

        user.SetToken(request);

        user = await _repository.UpdateAsync(user);

        return user.ToData();
    }

    public async Task Logout(int id)
    {
        var filter = new UserFilter
        {
            Id = id
        };
        var user = await GetUserOrThrow(filter);

        user.Logout();
        await _repository.UpdateAsync(user);
        await SaveChanges();
    }

    public async Task<UserData> Register(RegisterAggregate registerAggregate)
    {
        var companyRequest = registerAggregate.ToCompanyRequest();
        var companyData = await _companyService.Create(companyRequest);
        var company = await _companyRepository.BuIdAsync(companyData.Id);
        var role = company!.Roles.First();

        var userRequest = registerAggregate.ToUserRequest();
        userRequest.RoleId = role.Id;

        var userData = await _userService.CreateForCompany(userRequest, company, role);

        return await Login(registerAggregate.ToLoginRequest());
    }

    private async Task<User> GetUserOrThrow(UserFilter filter)
    {
        var user = await _repository.GetAsync(filter);
        if (user is null)
        {
            throw new BusinessException("Пользователь не найден");
        }

        return user;
    }
}
