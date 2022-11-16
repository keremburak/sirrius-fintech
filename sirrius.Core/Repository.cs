using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using sirrius.Data.Entity;
using sirrius.Data;

namespace sirrius.Core
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;

        //public BaseRepository(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}

        public TEntity Find(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool hideDeleted = true)
        {
            using var db = new sirriusContext();

            IQueryable<TEntity> query = db.Set<TEntity>();
            if (!hideDeleted)
                query = query.IgnoreQueryFilters();

            if (include != null)
                query = hideDeleted ? include(query) : include(query).IgnoreQueryFilters();

            return filter == null
                    ? query.FirstOrDefault()
                    : query.FirstOrDefault(filter);
        }


        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool hideDeleted = true)
        {
            using var db = new sirriusContext();
            IQueryable<TEntity> query = db.Set<TEntity>();
            if (!hideDeleted)
                query = query.IgnoreQueryFilters();

            if (include != null)
                query = hideDeleted ? include(query) : include(query).IgnoreQueryFilters();

            return filter == null
                    ? await query.FirstOrDefaultAsync()
                    : await query.FirstOrDefaultAsync(filter);
        }

        public TEntity FindById(TKey Id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool hideDeleted = true)
        {
            using var db = new sirriusContext();
            IQueryable<TEntity> query = db.Set<TEntity>();
            if (!hideDeleted)
                query = query.IgnoreQueryFilters();

            if (include != null)
                query = include(query);

            return query.SingleOrDefault(x => x.Id.Equals(Id));
        }

        public async Task<TEntity> FindByIdAsync(TKey Id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool hideDeleted = true)
        {
            using var db = new sirriusContext();
            IQueryable<TEntity> query = db.Set<TEntity>();
            if (!hideDeleted)
                query = query.IgnoreQueryFilters();

            if (include != null)
                query = include(query);

            return await query.SingleOrDefaultAsync(x => x.Id.Equals(Id));
        }

        public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool hideDeleted = true)
        {
            using var db = new sirriusContext();
            IQueryable<TEntity> query = db.Set<TEntity>();
            if (!hideDeleted)
                query = query.IgnoreQueryFilters();

            if (include != null)
                query = hideDeleted ? include(query) : include(query).IgnoreQueryFilters();

            return filter == null
                ? query.ToList()
                : query.Where(filter).ToList();
        }

        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool hideDeleted = true)
        {
            using var db = new sirriusContext();
            IQueryable<TEntity> query = db.Set<TEntity>();
            if (!hideDeleted)
                query = query.IgnoreQueryFilters();

            if (include != null)
                query = include(query);

            return filter == null
                ? await query.ToListAsync()
                : await query.Where(filter).ToListAsync();
        }

        public TEntity Insert(TEntity entity)
        {
            using var db = new sirriusContext();
            db.Set<TEntity>().Add(entity);
            db.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            using var db = new sirriusContext();
            db.Set<TEntity>().Update(entity);
            db.SaveChanges();
            return entity;
        }

        public bool BulkInsert(List<TEntity> list)
        {
            using var db = new sirriusContext();
            try
            {
                db.Set<TEntity>().AddRange(list);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool BulkUpdate(List<TEntity> list)
        {
            using var db = new sirriusContext();
            try
            {
                db.Set<TEntity>().UpdateRange(list);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool BulkDelete(List<TEntity> list)
        {
            using var db = new sirriusContext();
            try
            {
                //list.ForEach(x => x.Deleted = true);
                db.Set<TEntity>().UpdateRange(list);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool BulkDbDelete(List<TEntity> list)
        {
            using var db = new sirriusContext();
            try
            {
                db.Set<TEntity>().RemoveRange(list);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            using var db = new sirriusContext();
            db.Set<TEntity>().Add(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using var db = new sirriusContext();
            db.Set<TEntity>().Update(entity);
            await db.SaveChangesAsync();
            return entity;
        }


        public bool Delete(TEntity entity)
        {
            using var db = new sirriusContext();

            //entity.Deleted = true;
            db.Set<TEntity>().Update(entity);
            db.SaveChanges();
            return true;
        }

        public bool DeleteById(TKey id)
        {
            using var db = new sirriusContext();
            var entity = db.Set<TEntity>().FirstOrDefault(x => x.Id.Equals(id));
            if (entity != null)
            {
                //entity.Deleted = true;
                db.Set<TEntity>().Update(entity);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DbDelete(TEntity entity)
        {
            using var db = new sirriusContext();
            db.Set<TEntity>().Remove(entity);
            db.SaveChanges();
            return true;
        }

        public bool DbDeleteById(TKey id)
        {
            using var db = new sirriusContext();
            var entity = db.Set<TEntity>().FirstOrDefault(x => x.Id.Equals(id));
            if (entity != null)
            {
                db.Set<TEntity>().Remove(entity);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            using var db = new sirriusContext();

            //entity.Deleted = true;
            db.Set<TEntity>().Update(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DbDeleteAsync(TEntity entity)
        {
            using var db = new sirriusContext();
            db.Set<TEntity>().Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }

        //public bool IsNameExist(string name, int id)
        //{
        //    using var db = new sirriusContext();
        //    var entity = db.Set<TEntity>().Where(x => x.Id.ToString() != id.ToString() && x.Name.ToLower().Equals(name.ToLower()));

        //    if (entity != null && entity.Any())
        //        return true;

        //    return false;
        //}
    }
}
