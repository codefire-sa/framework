using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq.Expressions;
using Codefire.Storm.Actions;
using Codefire.Storm.Engine;
using Codefire.Storm.Querying;

namespace Codefire.Storm
{
    public class StormContext : IDataContext, IContextInitializer
    {
        #region [ Fields ]

        private IDataProvider _provider;
        private IInterceptor _interceptor;
        private EntityContainer _container;
        private SqlQueryManager _sqlManager;
        private Dictionary<Type, object> _repositories;
        private object _currentUser;

        #endregion

        #region [ Constructors ]

        public StormContext()
        {
            _provider = new SqlDataProvider();
            _sqlManager = new SqlQueryManager(_provider);
            _interceptor = new DefaultInterceptor();
            _repositories = new Dictionary<Type, object>();
            _container = StormContainer.Get(this.GetType());
        }

        public StormContext(string connectionName)
            : this()
        {
            var connectionItem = ConfigurationManager.ConnectionStrings[connectionName];
            if (connectionItem != null)
            {
                _provider.Connection.ConnectionString = connectionItem.ConnectionString;
            }
        }

        #endregion

        #region [ Properties ]

        public IDataProvider Provider
        {
            get { return _provider; }
        }

        public IInterceptor Interceptor
        {
            get { return _interceptor; }
            set { _interceptor = value; }
        }

        public string ConnectionString
        {
            get { return _provider.Connection.ConnectionString; }
            set { _provider.Connection.ConnectionString = value; }
        }

        public SqlQueryManager Sql
        {
            get { return _sqlManager; }
        }

        public object CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        #endregion

        #region [ Methods ]

        public EntityContainer BuildContainer()
        {
            var builder = new ModelBuilder();

            OnModelCreating(builder);

            return builder.Build();
        }

        protected virtual void OnModelCreating(ModelBuilder builder)
        {
        }

        public void BeginTransaction()
        {
            BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void BeginTransaction(IsolationLevel level)
        {
            _provider.BeginTransaction(level);
        }

        public void Commit()
        {
            _provider.CommitTransaction();
        }

        public void Rollback()
        {
            _provider.RollbackTransaction();
        }

        public EntityModel GetModel(Type entityType)
        {
            return _container.Get(entityType);
        }

        public EntityModel GetModel<TEntity>()
        {
            return GetModel(typeof(TEntity));
        }

        public IDataCommand CreateCommand(string commandText, CommandType commandType)
        {
            var cmd = new StormCommand(_provider);
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            return cmd;
        }

        public TEntity GetById<TEntity>(object id)
        {
            var action = new LoadAction<TEntity>(this);
            return action.GetById(id);
        }

        public TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> expression)
        {
            var qry = new EntityQuery<TEntity>(this);

            return qry.Where(expression).Single();
        }

        public IEntityQuery<TEntity> Find<TEntity>()
        {
            return new EntityQuery<TEntity>(this);
        }

        public object Insert<TEntity>(TEntity entity)
        {
            var action = new InsertAction<TEntity>(this);
            return action.Insert(entity);
        }

        public void Update<TEntity>(TEntity entity)
        {
            var action = new UpdateAction<TEntity>(this);
            action.Update(entity);
        }

        public void Delete<TEntity>(object id)
        {
            var action = new DeleteAction<TEntity>(this);
            action.Delete(id);
        }

        public void Reinstate<TEntity>(object id)
        {
            var action = new ReinstateAction<TEntity>(this);
            action.Reinstate(id);
        }

        protected TRepository GetRepository<TRepository>() where TRepository : IRepository, new()
        {
            TRepository repositoryObj;
            var repositoryType = typeof(TRepository);

            if (_repositories.ContainsKey(repositoryType))
            {
                repositoryObj = (TRepository)_repositories[repositoryType];
            }
            else
            {
                repositoryObj = new TRepository();
                repositoryObj.Initialize(this);

                _repositories.Add(repositoryType, repositoryObj);
            }

            return repositoryObj;
        }

        public void Dispose()
        {
            _provider.Dispose();
        }

        #endregion
    }
}