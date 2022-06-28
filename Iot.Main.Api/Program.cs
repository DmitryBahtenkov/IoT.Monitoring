using System.Reflection;
using Iot.Main.Api.Filters;
using System.Net;
using Iot.Main.Domain.Models.AlertModel;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.ConstraintModel;
using Iot.Main.Domain.Models.DeviceModel;
using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Models.UserModel;
using Iot.Main.Domain.Services;
using Iot.Main.Domain.Shared.Options;
using Iot.Main.Domain.Shared.Units;
using Iot.Main.Infrastructure.Commands;
using Iot.Main.Infrastructure.DataAccess;
using Iot.Main.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Iot.Main.Infrastructure.DataRecive;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(x => x.Filters.Add<ExceptionFilter>());
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IoT", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.KnownProxies.Add(IPAddress.Parse("78.140.241.46"));
});

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials());

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
    services.AddScoped<IRepository<Company>, CompanyRepository>();
    services.AddScoped<IRepository<User>, UserRepository>();
    services.AddScoped<IRepository<Device>, DeviceRepository>();
    services.AddScoped<IRepository<Constraint>, ConstraintRepository>();
    services.AddScoped<IRepository<Alert>, AlertRepository>();
    services.AddScoped<IRepository<Role>, RoleRepository>();

    services.AddDbContext<IEFContext, EFContext>();

    services.AddScoped<ICompanyService, CompanyService>();
    services.AddScoped<IAuthService, AuthService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IDeviceService, DeviceService>();
    services.AddScoped<IConstraintService, ConstraintService>();
    services.AddScoped<IAlertService, AlertService>();
    services.AddScoped<IRoleService, RoleService>();

    services.AddScoped<GetCompanyOrThrowCommand>();
    services.AddScoped<SendEmailCommand>();

    services.AddScoped<ICurrentUserService, CurrentUserService>();
    services.AddScoped<InfluxDbCommand>();
    services.AddScoped<GetInfluxDataCommand>();
}
