using LibrarySystemMinimalApi.Data.Context;
using LibrarySystemMinimalApi.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly LibraryDbContext context;
        protected readonly DbSet<T> dbSet;

        public BaseRepository(LibraryDbContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));
            dbSet = context.Set<T>();
        }

        // Basic CRUD Operations
        public virtual void Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            dbSet.Update(entity);
        }

        public virtual void Remove(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            dbSet.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            dbSet.RemoveRange(entities);
        }

        // Query Operations
        public virtual T GetById(params object[] keyValues)
        {
            return dbSet.Find(keyValues);
        }

        public virtual T GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return dbSet.FirstOrDefault(predicate);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }

        // Async Operations
        public virtual async Task<T> GetByIdAsync(params object[] keyValues)
        {
            return await dbSet.FindAsync(keyValues);
        }

        public virtual async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        // Utility Methods
        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Any(predicate);
        }

        public virtual int Count()
        {
            return dbSet.Count();
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Count(predicate);
        }

        // Save Changes
        public virtual void SaveChanges()
        {
            context.SaveChanges();
        }

        public virtual async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
