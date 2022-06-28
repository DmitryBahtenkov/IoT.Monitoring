using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.AlertModel;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.ConstraintModel;
using Iot.Main.Domain.Models.DeviceModel;
using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Models.UserModel;
using Iot.Main.Domain.Services;
using Iot.Main.Domain.Shared.Units;
using Iot.Main.Infrastructure.Commands;
using Iot.Main.Infrastructure.DataAccess;
using Iot.Main.Infrastructure.Services;
using Iot.Main.Tests.Infrastructure.Mocks;
using Microsoft.Extensions.DependencyInjection;

namespace Iot.Main.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IRepository<Company>, CompanyRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Device>, DeviceRepository>();
            services.AddScoped<IRepository<Constraint>, ConstraintRepository>();
            services.AddScoped<IRepository<Alert>, AlertRepository>();
            services.AddScoped<IRepository<Role>, RoleRepository>();

            services.AddDbContext<IEFContext, EFContextMock>();

            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IConstraintService, ConstraintService>();
            services.AddScoped<IAlertService, AlertService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<GetCompanyOrThrowCommand>();

            services.AddScoped<ICurrentUserService, CurrentUserServiceMock>();
        }
    }
}