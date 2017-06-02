using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NAlex.DataModel.Entities;
using NAlex.Selling.DTO.Interfaces;

namespace NAlex.Selling.DAL.Repositories
{
    public interface IRepository<TEntity, TKey>
        where TEntity: class
    {
        TEntity Get(TKey Id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> condition);
        TEntity Add(TEntity entity);
        TEntity Remove(TEntity entity);
        TEntity Remove(TKey Id);
        bool Update(TEntity entity);
    }
}
