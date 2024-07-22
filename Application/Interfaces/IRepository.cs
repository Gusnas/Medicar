using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Dispose();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string[] includeProperties = null);
        IEnumerable<TResult> Get<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string[] includeProperties = null);
        TEntity GetByID(object id);
        int Count(Expression<Func<TEntity, bool>> filter = null);
        void Insert(TEntity entity);
        void Save();
        void Update(TEntity entityToUpdate);
    }
}
