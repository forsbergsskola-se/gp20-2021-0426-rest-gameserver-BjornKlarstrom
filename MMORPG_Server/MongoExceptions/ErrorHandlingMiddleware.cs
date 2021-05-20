using System;
using Microsoft.AspNetCore.Mvc;

namespace MMORPG.MongoExceptions{
    public static class ErrorHandlingMiddleware{
        
        enum ErrorCode{
            BadRequest = 400,
            NotFound = 404,
            ServerTimeOut = 408,
        }
        
        public static IActionResult GetHttpCodeStatus(this Exception exception){
            return exception switch{
                NotFoundException => new StatusCodeResult((int) ErrorCode.NotFound),
                RequestTimeOutException => new StatusCodeResult((int) ErrorCode.ServerTimeOut),
                _ => new StatusCodeResult((int) ErrorCode.BadRequest)
            };
        }
    }
}
