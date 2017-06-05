using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using AutoMapper.Mappers;
using AutoMapper.QueryableExtensions;
using System.ComponentModel.DataAnnotations;

namespace NAlex.Selling.DAL.Repositories
{
    public abstract class DtoRepository<TEntity, TDto, TKey> : IRepository<TDto, TKey>
        where TDto : class
        where TEntity: class
    {
        protected DbContext _context;        

        public DtoRepository(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;

            Mapper.CreateMap<TEntity, TDto>();
            Mapper.CreateMap<TDto, TEntity>();
        }

        public virtual TDto Get(TKey Id)
        {
            return Mapper.Map<TEntity, TDto>(_context.Set<TEntity>().Find(Id));
        }

        public virtual IEnumerable<TDto> GetAll()
        {
            return _context.Set<TEntity>().UseAsDataSource().For<TDto>().ToArray();
        }

        public virtual IEnumerable<TDto> Get(System.Linq.Expressions.Expression<Func<TDto, bool>> condition,
            Func<IQueryable<TDto>, IOrderedQueryable<TDto>> orderBy = null)
        {
            var q = _context.Set<TEntity>().UseAsDataSource().For<TDto>().Where(condition);

            if (orderBy != null)
                return orderBy(q).ToArray();

            return q.ToArray();
        }

        public virtual TDto Add(TDto entity)
        {
            if (entity == null)
                return null;            

            if (_context.Set<TEntity>().Local.AsQueryable().UseAsDataSource().For<TDto>().Where(d => d.Equals(entity)).Any())
                return entity;

            TEntity newEntity = Mapper.Map<TEntity>(entity);

            return Mapper.Map<TDto>(_context.Set<TEntity>().Add(newEntity));
        }

        public virtual TDto Remove(TDto entity)
        {
            if (entity == null)
                return null;
            TEntity newEntity = Mapper.Map<TEntity>(entity);
            TEntity attached = _context.Set<TEntity>().Attach(newEntity);
            _context.Entry<TEntity>(newEntity).State = EntityState.Deleted;
            
            return Mapper.Map<TDto>(_context.Set<TEntity>().Remove(attached));
        }

        public virtual TDto Remove(TKey Id)
        {
            TDto entity = Get(Id);
            if (entity != null)
                return Remove(entity);

            return null;
        }

        public virtual bool Update(TDto entity)
        {
            if (entity == null)
                return false;

            TEntity newEntity = Mapper.Map<TEntity>(entity);
            var props = newEntity.GetType()
                .GetProperties()
                .FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Any());
            if (props == null)
                return false;

            TEntity existing = _context.Set<TEntity>().Find(props.GetValue(newEntity, null));
            if (existing == null)
                return false;

            _context.Entry<TEntity>(existing).CurrentValues.SetValues(newEntity);
            _context.Entry<TEntity>(existing).State = EntityState.Modified;

            return true;
        }
    }


}
