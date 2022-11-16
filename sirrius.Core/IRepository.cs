using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Core
{
   public interface IRepository<T, TKey> where T : class
    {
        T Find(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool hideDeleted = true);
        T FindById(TKey Id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool hideDeleted = true);
        ICollection<T> FindAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool hideDeleted = true);

        Task<T> FindAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool hideDeleted = true);
        Task<T> FindByIdAsync(TKey Id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool hideDeleted = true);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool hideDeleted = true);

        T Insert(T entity);
        T Update(T entity);
        bool Delete(T entity);
        bool DeleteById(TKey Id);

        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DbDeleteAsync(T entity);

        bool DbDelete(T entity);
        bool DbDeleteById(TKey Id);

        bool BulkInsert(List<T> list);
        bool BulkUpdate(List<T> list);
        bool BulkDelete(List<T> list);
        bool BulkDbDelete(List<T> list);
    }
}
