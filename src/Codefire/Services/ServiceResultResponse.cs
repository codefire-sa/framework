using System;
using System.Runtime.Serialization;

namespace Codefire.Services
{
    [DataContract]
    public class ServiceResultResponse<T> : ServiceResponse
    {
        #region [ Fields ]

        private T _result;

        #endregion

        #region [ Constructors ]

        public ServiceResultResponse()
            : base()
        {
        }

        public ServiceResultResponse(T result)
            : base()
        {
            _result = result;
        }

        public ServiceResultResponse(int errorCode, string errorMessage, T result)
            : base(errorCode, errorMessage)
        {
            _result = result;
        }

        #endregion

        #region [ Properties ]

        [DataMember]
        public T Result
        {
            get { return _result; }
            set { _result = value; }
        }

        #endregion
    }
}