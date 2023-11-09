using DALTestsDB;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class GenericUnitOfWork : IGenericUnitOfWork
    {
        DbContext context;
        private Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        public GenericUnitOfWork()
        {
            context = new TestDBContext();
        }
        public GenericUnitOfWork(DbContext context)
        {
            this.context = context;
        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (repositories.Keys.Contains(typeof(TEntity)))
            {
                return repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
            }
            IGenericRepository<TEntity> newRep = new EFGenericRepository<TEntity>(context);
            repositories.Add(typeof(TEntity), newRep);
            return newRep;
        }        
        void IDisposable.Dispose()
        {
            context.Dispose();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}