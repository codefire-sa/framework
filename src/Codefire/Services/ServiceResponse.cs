using System;
using System.Runtime.Serialization;

namespace Codefire.Services
{
    [DataContract]
    public class ServiceResponse
    {
        #region [ Fields ]

        private int _errorCode;
        private string _errorMessage = "";

        #endregion

        #region [ Constructors ]

        public ServiceResponse()
        {
        }

        public ServiceResponse(int errorCode, string errorMessage)
        {
            _errorCode = errorCode;
            _errorMessage = errorMessage;
        }

        #endregion

        #region [ Properties ]

        [DataMember]
        public int ErrorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }

        [DataMember]
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        #endregion
    }
}
