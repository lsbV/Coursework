using DALTestsDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repository
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        DbContext context;
        DbSet<TEntity> dbSet;

        public EFGenericRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }


        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToArrayAsync();            
        }

        public TEntity? FindById(params object[] id)
        {
            return dbSet.Find(id);
        }

        public async Task<TEntity?> FindByIdAsync(params object[] id)
        {
            return await dbSet.FindAsync(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
            context.SaveChanges();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            await Task.Run(() => dbSet.Remove(entity));
            await context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => context.Update(entity));
            await context.SaveChangesAsync();
        }

        public IEnumerable<TEntity> LoadCollection(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = dbSet.AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query.Where(filter).ToList();
        }
        //public async Task LoadAssociatedCollectionAsync(TEntity entity, string propertyName)
        //{
        //    dbSet.Attach(entity);
        //    await dbSet.Entry(entity).Collection(propertyName).LoadAsync();
        //}
        public async Task LoadAssociatedCollectionAsync(TEntity entity, Expression<Func<TEntity, IEnumerable<object>>> expression)
        {
            dbSet.Attach(entity);
            await dbSet.Entry(entity).Collection(expression).LoadAsync();
        }
        //public async Task LoadAssociatedCollection(TEntity entity, Expression<Func<TEntity, IEnumerable<object>>> expression, Expression<Func<object, object>> thenInclude)
        //{
        //    dbSet.Attach(entity);
        //    await dbSet.Entry(entity).Collection(expression).Query().Include(thenInclude).LoadAsync();
        //}

        public async Task LoadAssociatedPropertyAsync(TEntity entity, Expression<Func<TEntity, object>> expression)
        {
            dbSet.Attach(entity);
            await dbSet.Entry(entity).Reference(expression).LoadAsync();
        }   

    }
   
}