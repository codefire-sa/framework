using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Codefire.Services
{
    public class ServiceProxy<T> : IDisposable where T : class
    {
        private ChannelFactory<T> _factory;
        private T _proxy;

        public ServiceProxy(string name)
        {
            _factory = new ChannelFactory<T>(name);
            _proxy = _factory.CreateChannel();
        }

        public T Service
        {
            get { return _proxy; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            _factory.Close();
        }

        #endregion
    }
}