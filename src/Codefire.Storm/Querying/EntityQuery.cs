using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Codefire.Storm.Engine;
using Codefire.Storm.Actions;

namespace Codefire.Storm.Querying
{
    public class EntityQuery<TEntity> : IEntityQuery<TEntity>
    {
        #region [ Fields ]

        private IDataContext _context;
        private LoadAction<TEntity> _action;
        private EntityQueryValues _values;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public EntityQuery(IDataContext context)
        {
            var model = context.GetModel<TEntity>();

            _context = context;
            _action = new LoadAction<TEntity>(context);
            _values = new EntityQueryValues();
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets an instance of the data provider associated with this query.
        /// </summary>
        public IDataContext Context
        {
            get { return _context; }
        }

        /// <summary>
        /// 
        /// </summary>
        public EntityQueryValues Values
        {
            get { return _values; }
        }

        #endregion

        #region [ Methods ]

        public IEntityQuery<TEntity> Top(int limit)
        {
            _values.Limit = limit;
            return this;
        }

        #region [ Criteria ]

        public IEntityQuery<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            var parser = new CriteriaExpressionVisitor();
            parser.Parse(expression, CriteriaType.And);
            _values.Criteria.AddRange(parser.Criteria);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public IEntityQuery<TEntity> Where(Expression<Func<TEntity, object>> expression, Criteria constraint)
        {
            var visitor = new PropertyExpressionVisitor();
            visitor.Parse(expression);

            constraint.CriteriaType = CriteriaType.And;
            constraint.ColumnName = visitor.Name;
            _values.Criteria.Add(constraint);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public IEntityQuery<TEntity> Where(string memberName, Criteria constraint)
        {
            constraint.CriteriaType = CriteriaType.And;
            constraint.ColumnName = memberName;
            _values.Criteria.Add(constraint);

            return this;
        }

        public IEntityQuery<TEntity> And(Expression<Func<TEntity, bool>> expression)
        {
            var parser = new CriteriaExpressionVisitor();
            parser.Parse(expression, CriteriaType.And);
            _values.Criteria.AddRange(parser.Criteria);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public IEntityQuery<TEntity> And(Expression<Func<TEntity, object>> expression, Criteria constraint)
        {
            var visitor = new PropertyExpressionVisitor();
            visitor.Parse(expression);

            constraint.CriteriaType = CriteriaType.And;
            constraint.ColumnName = visitor.Name; ;
            _values.Criteria.Add(constraint);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public IEntityQuery<TEntity> And(string memberName, Criteria constraint)
        {
            constraint.CriteriaType = CriteriaType.And;
            constraint.ColumnName = memberName;
            _values.Criteria.Add(constraint);

            return this;
        }

        public IEntityQuery<TEntity> Or(Expression<Func<TEntity, bool>> expression)
        {
            var parser = new CriteriaExpressionVisitor();
            parser.Parse(expression, CriteriaType.Or);
            _values.Criteria.AddRange(parser.Criteria);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public IEntityQuery<TEntity> Or(Expression<Func<TEntity, object>> expression, Criteria constraint)
        {
            var visitor = new PropertyExpressionVisitor();
            visitor.Parse(expression);

            constraint.CriteriaType = CriteriaType.Or;
            constraint.ColumnName = visitor.Name; ;
            _values.Criteria.Add(constraint);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public IEntityQuery<TEntity> Or(string memberName, Criteria constraint)
        {
            constraint.CriteriaType = CriteriaType.Or;
            constraint.ColumnName = memberName;
            _values.Criteria.Add(constraint);

            return this;
        }

        #endregion

        #region [ Order By ]

        /// <summary>
        /// Sets the list of columns to be ordered by in the select.
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public IEntityQuery<TEntity> OrderAsc(params string[] memberList)
        {
            foreach (var memberName in memberList)
            {
                _values.OrderBy.Add(memberName, true);
            }

            return this;
        }

        /// <summary>
        /// Sets the list of columns to be ordered by in the select.
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public IEntityQuery<TEntity> OrderDesc(params string[] memberList)
        {
            foreach (var memberName in memberList)
            {
                _values.OrderBy.Add(memberName, false);
            }

            return this;
        }

        public IEntityQuery<TEntity> OrderAsc(params Expression<Func<TEntity, object>>[] expressionList)
        {
            var visitor = new PropertyExpressionVisitor();

            foreach (var expression in expressionList)
            {
                visitor.Parse(expression);
                _values.OrderBy.Add(visitor.Name, true);
            }

            return this;
        }

        public IEntityQuery<TEntity> OrderDesc(params Expression<Func<TEntity, object>>[] expressionList)
        {
            var visitor = new PropertyExpressionVisitor();

            foreach (var expression in expressionList)
            {
                visitor.Parse(expression);
                _values.OrderBy.Add(visitor.Name, false);
            }

            return this;
        }

        #endregion

        #region [ Data Commands ]

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public TEntity Single()
        {
            return _action.FindOne(_values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TEntity> List()
        {
            return _action.FindMany(_values);
        }

        #endregion

        #endregion
    }
}