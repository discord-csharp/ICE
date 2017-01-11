using System;

namespace Ice.Services
{
    public class IceServiceException : Exception
    {
        public IceServiceException() : base() { }

        public IceServiceException(string message) : base(message) { }

        public IceServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
