using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Data.Repositories.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        // Basic CRUD Operations
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);


        // Query Operations
        T GetById(params object[] keyValues);
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);


        // Async Operations
        Task<T> GetByIdAsync(params object[] keyValues);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);


        // Additional utility methods
        bool Exists(Expression<Func<T, bool>> predicate);
        int Count();
        int Count(Expression<Func<T, bool>> predicate);


        // Save changes
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
