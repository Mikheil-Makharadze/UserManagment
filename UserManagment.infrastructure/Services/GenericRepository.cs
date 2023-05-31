using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserManagment.Core.Entities;
using UserManagment.Core.Interfaces;
using UserManagment.infrastructure.Data.DB;

namespace UserManagment.infrastructure.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext Context;
        internal DbSet<T> dbSet;

        public GenericRepository(AppDbContext context)
        {
            Context = context;
            dbSet = Context.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await dbSet.ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id) => await dbSet.FirstOrDefaultAsync(n => n.Id == id);

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await SaveAsync();

            return entity;
        }

        protected async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
