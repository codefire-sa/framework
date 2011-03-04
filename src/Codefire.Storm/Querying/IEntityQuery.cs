using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Codefire.Collections;

namespace Codefire.Storm.Querying
{
    public interface IEntityQuery<TEntity>
    {
        IDataContext Context { get; }

        IEntityQuery<TEntity> Top(int limit);
        IEntityQuery<TEntity> Where(Expression<Func<TEntity, bool>> expression);
        IEntityQuery<TEntity> Where(Expression<Func<TEntity, object>> expression, Criteria constraint);
        IEntityQuery<TEntity> Where(string memberName, Criteria constraint);
        IEntityQuery<TEntity> And(Expression<Func<TEntity, bool>> expression);
        IEntityQuery<TEntity> And(Expression<Func<TEntity, object>> expression, Criteria constraint);
        IEntityQuery<TEntity> And(string memberName, Criteria constraint);
        IEntityQuery<TEntity> Or(Expression<Func<TEntity, bool>> expression);
        IEntityQuery<TEntity> Or(Expression<Func<TEntity, object>> expression, Criteria constraint);
        IEntityQuery<TEntity> Or(string memberName, Criteria constraint);
        IEntityQuery<TEntity> OrderAsc(params Expression<Func<TEntity, object>>[] expressionList);
        IEntityQuery<TEntity> OrderAsc(params string[] memberList);
        IEntityQuery<TEntity> OrderDesc(params Expression<Func<TEntity, object>>[] expressionList);
        IEntityQuery<TEntity> OrderDesc(params string[] memberList);

        TEntity Single();
        List<TEntity> List();
        PagedList<TEntity> PagedList(int pageNumber, int pageSize);
        int Count();
    }
}
