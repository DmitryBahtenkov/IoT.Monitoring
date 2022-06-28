using Iot.Main.Domain.Shared;
using Iot.Main.Domain.Shared.Units;

namespace Iot.Main.Infrastructure.DataAccess;

public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : BaseEntity
{
    private readonly IEFContext _eFContext;
    private readonly IRepository<TEntity> _repository;

    public UnitOfWork(
        IEFContext eFContext,
        IRepository<TEntity> repository)
    {
        _repository = repository;
        _eFContext = eFContext;
    }

    public IRepository<TEntity> GetRepository()
    {
        return _repository;
    }

    public async Task SaveChangesAsync()
    {
        await _eFContext.SaveChangesAsync();
    }
}
