using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using System;
using DomainEntities.Commons;

namespace DomainContracts.Commons
{
    public interface IRepository<T> where T : BaseEntity
    {
        //T GetById(int id);
        T GetSingleBySpec(ISpecification<T> spec);
        IList<T> ListAll();
        IList<T> List(ISpecification<T> spec);
        T Add(T entity);
        void Update(T entity, params Expression<Func<T, object>>[] updatedProperties);
        void Delete(T entity);
        IDbContextTransaction BeginTransaction();
        void AddList(IEnumerable<T> entity);
        T GetSingleBySpec(Expression<Func<T, bool>> criteria);
    }
}