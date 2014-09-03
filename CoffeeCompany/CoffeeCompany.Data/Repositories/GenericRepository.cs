namespace CoffeeCompany.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;

    using CoffeeCompany.Data;

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ICoffeeCompanyDbContext context;
        private IDbSet<T> set;

        public GenericRepository(ICoffeeCompanyDbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        public IQueryable<T> All()
        {
            return this.set.AsQueryable();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> conditions)
        {
            return this.All().Where(conditions);
        }

        public bool Any(Expression<Func<T, bool>> conditions)
        {
            return this.All().Any(conditions);
        }

        public void Add(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Added;
        }

        public void Update(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Deleted;
        }

        public void Detach(T entity)
        {
            var entry = this.context.Entry(entity);
            entry.State = EntityState.Detached;
        }

        private DbEntityEntry AttachIfDetached(T entity)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            return entry;
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
