using System;

namespace Codefire.Storm
{
    public interface IRepository
    {
        IDataContext Context { get; }

        void Initialize(IDataContext context);
    }
}