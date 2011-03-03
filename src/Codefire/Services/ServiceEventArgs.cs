using System;

namespace Codefire.Services
{
    public class ServiceEventArgs : EventArgs
    {
        #region [ Fields ]

        private ServiceResponse _response;

        #endregion

        #region [ Constructors ]

        public ServiceEventArgs(ServiceResponse response)
        {
            _response = response;
        }

        #endregion

        #region [ Properties ]

        public int ErrorCode
        {
            get { return _response.ErrorCode; }
        }

        public string ErrorMessage
        {
            get { return _response.ErrorMessage; }
        }

        public T GetResult<T>()
        {
            var typedResult = _response as ServiceResultResponse<T>;
            if (typedResult == null)
            {
                return default(T);
            }
            else
            {
                return typedResult.Result;
            }
        }

        #endregion
    }
}