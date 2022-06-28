using Iot.Main.Domain.Shared;
using Iot.Main.Domain.Shared.Exceptions;
using Iot.Main.Domain.Shared.Units;

namespace Iot.Main.Infrastructure.Services
{
    public abstract class BaseService<TEntity> where TEntity : BaseEntity
    {
        protected readonly IUnitOfWork<TEntity> UnitOfWork;

        protected BaseService(IUnitOfWork<TEntity> unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<TEntity> Archive(int id)
        {
            var reposiitory = UnitOfWork.GetRepository();
            var entity = await reposiitory.BuIdAsync(id);

            if (entity is null)
            {
                throw new BusinessException("Не найдена сущность");
            }

            entity.SetArchived(true);
            entity = await reposiitory.UpdateAsync(entity);
            await SaveChanges();

            return entity;
        }

        protected async Task SaveChanges()
        {
            await UnitOfWork.SaveChangesAsync();
        }
    }
}