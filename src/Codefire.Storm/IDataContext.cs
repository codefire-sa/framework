using System;
using System.Data;
using System.Linq.Expressions;
using Codefire.Storm.Engine;
using Codefire.Storm.Querying;

namespace Codefire.Storm
{
    public interface IDataContext : IDisposable
    {
        IDataProvider Provider { get; }
        IInterceptor Interceptor { get; set; }
        string ConnectionString { get; set; }
        SqlQueryManager Sql { get; }
        object CurrentUser { get; set; }

        void BeginTransaction();
        void BeginTransaction(IsolationLevel level);
        void Commit();
        void Rollback();

        EntityModel GetModel(Type entityType);
        EntityModel GetModel<TEntity>();
        IDataCommand CreateCommand(string commandText, CommandType commandType);

        TEntity GetById<TEntity>(object id);
        TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> expression);
        IEntityQuery<TEntity> Find<TEntity>();
        object Insert<TEntity>(TEntity entity);
        void Update<TEntity>(TEntity entity);
        void Delete<TEntity>(object id);
        void Reinstate<TEntity>(object id);
    }
}
