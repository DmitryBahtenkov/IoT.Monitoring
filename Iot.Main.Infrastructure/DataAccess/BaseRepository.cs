using System.Linq.Expressions;
using Iot.Main.Domain.Services;
using Iot.Main.Domain.Shared;
using Iot.Main.Domain.Shared.Units;
using Microsoft.EntityFrameworkCore;

namespace Iot.Main.Infrastructure.DataAccess;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly IEFContext _eFContext;
    private readonly ICurrentUserService _currentUserService;
    protected readonly DbSet<TEntity> Set;

    public BaseRepository(
        IEFContext eFContext,
        ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        _eFContext = eFContext;
        Set = _eFContext.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        var entry = await Set.AddAsync(entity);

        return entry.Entity;
    }

    public Task DeleteAsync(TEntity entity)
    {
        Set.Remove(entity);

        return Task.CompletedTask;
    }

    public async Task<TEntity?> GetAsync(IFilter<TEntity> filter)
    {
        return await Set
            .Where(GetSecurityExpression())
            .FirstOrDefaultAsync(filter.ToExpression());
    }

    public async Task<List<TEntity>> ListAsync(IFilter<TEntity> filter)
    {
        return await Set
            .Where(filter.ToExpression())
            .Where(GetSecurityExpression())
            .ToListAsync();
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        var entry = Set.Update(entity);

        return Task.FromResult(entry.Entity);
    }

    private Expression<Func<TEntity, bool>> GetSecurityExpression()
    {
        if (_currentUserService.CompanyId is not null)
        {
            if (typeof(IEntityWithCompanyId).IsAssignableFrom(typeof(TEntity)))
            {
                return x => ((IEntityWithCompanyId)x).CompanyId == _currentUserService.CompanyId;
            }

            if (typeof(IEntityWithNotRequiredCompanyId).IsAssignableFrom(typeof(TEntity)))
            {
                return x => ((IEntityWithNotRequiredCompanyId)x).CompanyId == _currentUserService.CompanyId;
            }
        }

        return _ => true;
    }

    public async Task<TEntity?> BuIdAsync(int id)
    {
        return await Set
            .Where(GetSecurityExpression())
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<TEntity>> ListByIdsAsync(params int[] ids)
    {
        return await Set
            .Where(GetSecurityExpression())
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }
}
