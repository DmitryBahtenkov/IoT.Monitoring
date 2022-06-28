#nullable disable
using Iot.Main.Domain.Models.AlertModel;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.ConstraintModel;
using Iot.Main.Domain.Models.DeviceModel;
using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Models.UserModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Iot.Main.Infrastructure.DataAccess;

public class EFContext : DbContext, IEFContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Constraint> Constraints { get; set; }
    public DbSet<Alert> Alerts { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer("");
    }
}
