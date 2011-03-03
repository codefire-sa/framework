using System;

namespace Codefire.Storm
{
    public abstract class BaseRepository : IRepository
    {
        private IDataContext _context;

        public BaseRepository()
        {
        }

        public IDataContext Context
        {
            get { return _context; }
        }

        public void Initialize(IDataContext context)
        {
            _context = context;
        }
    }
}