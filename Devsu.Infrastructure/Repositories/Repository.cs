using DataAccessLayer.Data;
using Microsoft.Extensions.Logging;
using System.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devsu.Infrastructure.Repositories;
using Devsu.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq.Expressions;
using HandlebarsDotNet;
using System.Reflection.Metadata;
using Devsu.Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.Query;

namespace Devsu.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {

        private readonly DbContext _DbContext;

        public Repository(DbContext context)
        {
            _DbContext = context;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(AddAsync)} entity no puede ser nulo");

            try
            {
                await _DbContext.AddAsync(entity);
                await _DbContext.SaveChangesAsync();
                return entity;

            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(AddAsync)} entity no pudo guardarse: {ex.Message}");
            }
        }

        public virtual async Task DeleteAsync(object id)
        {
            try
            {
                var entity = await _DbContext.FindAsync<TEntity>(id);
                if (entity == null)
                    throw new NullReferenceException($"{nameof(DeleteAsync)} entity no existe con el id indicado");

                _DbContext.Remove<TEntity>(entity);
                await _DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(DeleteAsync)} entity no pudo eliminarse: {ex.Message}");
            }

        }

        public virtual async Task<IList<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            try
            {
                var query = _DbContext.Set<TEntity>().Select(t => t);
                foreach (var includeExpression in includeExpressions)
                {
                    query = query.Include(includeExpression);
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(GetAllAsync)} no se pudo obtener los registros: {ex.Message}");
            }
        }
        public virtual async Task<TEntity> GetLastByParamAsync(string idName, object idvalue, params Expression<Func<TEntity, object>>[] orderExpressions)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(TEntity));
                var left = Expression.Property(parameter, idName);
                var right = Expression.Constant(idvalue);
                var equal = Expression.Equal(left, right);
                var byId = Expression.Lambda<Func<TEntity, bool>>(equal, parameter);

                var query = _DbContext.Set<TEntity>().Select(t => t);
                foreach (var orderExpression in orderExpressions)
                {
                    query = query.OrderByDescending(orderExpression);
                }

                return await query.FirstOrDefaultAsync(byId);
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(GetByIdAsync)} no se pudo obtener el registro: {ex.Message}");
            }
        }
        public virtual async Task<TEntity> GetByIdAsync(object id, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(TEntity));
                var left = Expression.Property(parameter, "Id");
                var right = Expression.Constant(id);
                var equal = Expression.Equal(left, right);
                var byId = Expression.Lambda<Func<TEntity, bool>>(equal, parameter);

                //var query = _DbContext.Set<TEntity>();//.Find(id);/
                var query = _DbContext.Set<TEntity>().Select(t => t);
                foreach (var includeExpression in includeExpressions)
                {
                    query = query.Include(includeExpression);
                }

                return await query.SingleOrDefaultAsync(byId);
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(GetByIdAsync)} no se pudo obtener el registro: {ex.Message}");
            }
        }

        public virtual async Task<List<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            try
            {
                var query = _DbContext.Set<TEntity>().Select(t => t);
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                foreach (var includeExpression in includeExpressions)
                {
                    query = query.Include(includeExpression);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(GetByIdAsync)} no se pudo obtener el registro: {ex.Message}");
            }
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(UpdateAsync)} entity no puede ser nulo");

            try
            {
                _DbContext.ChangeTracker.Clear();
                _DbContext.Update(entity);
                await _DbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(UpdateAsync)} entity no pudo actualizarse: {ex.Message}");
            }


        }

        public virtual async Task<TEntity> PatchAsync(Expression<Func<TEntity, bool>> filterId, JsonPatchDocument entityPatch, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            try
            {
                var query = _DbContext.Set<TEntity>().Select(t => t);
                if (filterId != null)
                {
                    query = query.Where(filterId);
                    foreach (var includeExpression in includeExpressions)
                    {
                        query = query.Include(includeExpression);
                    }
                    var cEntity = await query.FirstOrDefaultAsync();
                    if (cEntity == null)
                    {
                        return cEntity;
                    }
                    entityPatch.ApplyTo(cEntity);
                    await _DbContext.SaveChangesAsync();
                    return cEntity;
                }
                else
                {
                    throw new Exception($"{nameof(PatchAsync)} requiere el filtro de id de Identidad");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(UpdateAsync)} entity no pudo actualizarse: {ex.Message}");
            }


        }
    }
}
