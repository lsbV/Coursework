using DALTestsDB;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class GenericUnitOfWork : IGenericUnitOfWork
    {
        private readonly DbContext context;
        private readonly Dictionary<Type, object> repositories = new();
        private readonly object _lock = new();

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
            lock (_lock)
            {
                if (repositories.ContainsKey(typeof(TEntity)))
                {
                    if (repositories[typeof(TEntity)] == null) throw new NullReferenceException("Repository is null");
                    if (repositories[typeof(TEntity)] is IGenericRepository<TEntity> repo)
                        return repo;
                    throw new InvalidCastException("Repository is not IGenericRepository<TEntity>");
                }
                IGenericRepository<TEntity> newRep = new EFGenericRepository<TEntity>(context);
                repositories.Add(typeof(TEntity), newRep);
                return newRep;
            }                
        }
        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
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