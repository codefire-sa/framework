using System;
using Codefire.Storm.Engine;

namespace Codefire.Storm
{
    public interface IContextInitializer
    {
        EntityContainer BuildContainer();
    }
}