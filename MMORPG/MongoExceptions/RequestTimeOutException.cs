using System;

namespace MMORPG.MongoExceptions{
    public class RequestTimeOutException : Exception{
        
        public RequestTimeOutException() { }
        
        public RequestTimeOutException(string message)
            : base(message) { }
    }
}