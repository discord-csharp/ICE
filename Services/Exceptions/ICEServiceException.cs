using System;

namespace ICE.Services
{
    public class ICEServiceException : Exception
    {
        public ICEServiceException() : base() { }

        public ICEServiceException(string message) : base(message) { }

        public ICEServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
