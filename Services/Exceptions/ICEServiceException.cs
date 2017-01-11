using System;

namespace Ice.Services.Exceptions
{
    public class ICEServiceException : Exception
    {
        public ICEServiceException() : base() { }

        public ICEServiceException(string message) : base(message) { }

        public ICEServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
