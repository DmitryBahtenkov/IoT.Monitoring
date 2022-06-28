namespace Iot.Main.Domain.Shared.Units;

public interface IUnitOfWork<TEntity> where TEntity : BaseEntity
{
    public Task SaveChangesAsync();
    public IRepository<TEntity> GetRepository();
}
