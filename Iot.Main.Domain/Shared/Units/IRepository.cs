#nullable enable

namespace Iot.Main.Domain.Shared.Units;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);

    Task<TEntity?> GetAsync(IFilter<TEntity> filter);
    Task<TEntity?> BuIdAsync(int id);

    Task<List<TEntity>> ListAsync(IFilter<TEntity> filter);
    Task<List<TEntity>> ListByIdsAsync(params int[] ids);
}
