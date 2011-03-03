using System;

namespace Codefire.Storm.Querying
{
    public abstract class SqlQuery : ISqlQuery
    {
        private IDataProvider _provider;
        private QueryTemplate _template;

        protected SqlQuery(IDataProvider provider, QueryType type)
        {
            _provider = provider;
            _template = new QueryTemplate(type);
        }

        public IDataProvider Provider
        {
            get { return _provider; }
        }

        public QueryTemplate Template
        {
            get { return _template; }
        }

        public IDataCommand Build()
        {
            return _provider.Generate(_template);
        }
    }
}