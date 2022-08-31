using Infrastructure.Utilities.Errors;
using System;
using System.Text.Json;

namespace Infrastructure.Utilities.CustomException
{
    public class CustomException : Exception
    {
        public CustomException(ErrorCode errorCode, string message)
            : base(JsonSerializer.Serialize(new { errorCode = errorCode, message = message }))
        {
        }
    }
}
