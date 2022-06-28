using System.Linq.Expressions;

namespace Iot.Main.Domain.Shared;

public interface IFilter<TEntity> where TEntity : BaseEntity
{
    public static Expression<Func<TEntity, bool>> TrueExpression => x => !x.IsArchive;
    public int? Id { get; set; }
    public Expression<Func<TEntity, bool>> ToExpression();
}
