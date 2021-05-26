using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COVID19_API.Responses.COVID19_API
{
    public class BaseResponse<T> : BaseResponse
    {
        public IReadOnlyList<T> Data { get; set; }
    }

    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
        }
        public BaseResponse(string message = null)
        {
            Success = true;
            Message = message;
        }

        public BaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }

    }
}