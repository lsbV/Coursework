using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);  
        Task AddAsync(TEntity entity);

        TEntity? FindById(params object[] id);
        Task<TEntity?> FindByIdAsync(params object[] id);

        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);        
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

        void Remove(TEntity entity);
        Task RemoveAsync(TEntity entity);

        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);

        public Task LoadAssociatedCollectionAsync(TEntity entity, Expression<Func<TEntity, IEnumerable<object>>> expression);
        public void LoadAssociatedCollection(TEntity entity, Expression<Func<TEntity, IEnumerable<object>>> expression);

        public Task LoadAssociatedCollectionAsync(TEntity entity, Expression<Func<TEntity, IEnumerable<object>>> expression, Expression<Func<object, object>> thenInclude);
        public void LoadAssociatedCollection(TEntity entity, Expression<Func<TEntity, IEnumerable<object>>> expression, Expression<Func<object, object>> thenInclude);
        
        public Task LoadAssociatedPropertyAsync(TEntity entity, Expression<Func<TEntity, object?>> expression);
        public void LoadAssociatedProperty(TEntity entity, Expression<Func<TEntity, object?>> expression);
    }
}