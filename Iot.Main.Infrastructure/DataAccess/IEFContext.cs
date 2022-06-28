using Microsoft.EntityFrameworkCore;

namespace Iot.Main.Infrastructure.DataAccess;

public interface IEFContext
{
    public DbSet<TEntity> Set<TEntity>() where TEntity : class;
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public int SaveChanges();
}
