using System;
using Codefire.Storm.Engine;

namespace Codefire.Storm.Actions
{
    public abstract class DatabaseAction<TEntity>
    {
        private IDataContext _context;
        private EntityModel _model;

        protected DatabaseAction(IDataContext context)
        {
            _context = context;
            _model = context.GetModel<TEntity>();
        }

        protected IDataContext Context
        {
            get { return _context; }
        }

        protected EntityModel Model
        {
            get { return _model; }
        }
    }
}
