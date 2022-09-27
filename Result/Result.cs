using FluentValidation.Results;
using System.Net;
using System.Text.Json.Serialization;

namespace Mbis.Cognito.Result
{
    public class Result : IResult
    {
        public Result()
        {
        }

        [JsonInclude]
        public HttpStatusCode StatusCode { get; set; }

        [JsonInclude]
        public bool Failed => !Succeeded;

        [JsonInclude]
        public string Message { get; set; }

        [JsonInclude]
        public bool Succeeded { get; set; }

        [JsonInclude]
        public List<ValidationFailure> Errors { get; set; }

        public static IResult Fail(HttpStatusCode statusCode = HttpStatusCode.UnprocessableEntity)
        {
            return new Result { Succeeded = false, StatusCode = statusCode };
        }

        public static IResult Fail(string message, HttpStatusCode statusCode = HttpStatusCode.UnprocessableEntity)
        {
            return new Result { Succeeded = false, Message = message, StatusCode = statusCode };
        }

        public static IResult Success(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return new Result { Succeeded = true, StatusCode = httpStatusCode };
        }

        public static IResult Success(string message, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return new Result { Succeeded = true, Message = message, StatusCode = httpStatusCode };
        }

        internal static IResult Fail(List<ValidationFailure> errors)
        {
            return new Result { Errors = errors };
        }
    }

    public class Result<T> : Result, IResult<T>
    {

        public new static IResult<T> Fail(HttpStatusCode httpStatusCode = HttpStatusCode.UnprocessableEntity)
        {
            return new Result<T> { Succeeded = false, StatusCode = httpStatusCode };
        }

        /// <summary>
        /// Fail Result Text Only
        /// </summary>
        /// <param name="message"></param>
        /// <param name="httpStatusCode"></param>
        /// <returns></returns>
        public new static IResult<T> Fail(string message,
            HttpStatusCode httpStatusCode = HttpStatusCode.UnprocessableEntity)
        {
            return new Result<T> { Succeeded = false, Message = message, StatusCode = httpStatusCode };
        }

        /// <summary>
        /// Fail Result Object Only 
        /// </summary>
        /// <param name="error"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static IResult<T> Fail(T error, HttpStatusCode statusCode = HttpStatusCode.UnprocessableEntity)
        {
            return new Result<T> { Succeeded = false, Error = error, StatusCode = statusCode };
        }

        /// <summary>
        /// Fail Result Object with Text
        /// </summary>
        /// <param name="message"></param>
        /// <param name="error"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static IResult<T> Fail(string message, T error,
            HttpStatusCode statusCode = HttpStatusCode.UnprocessableEntity)
        {
            return new Result<T> { Succeeded = false, Message = message, Error = error, StatusCode = statusCode };
        }


        public new static IResult<T> Success(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return new Result<T> { Succeeded = true, StatusCode = httpStatusCode };
        }

        public new static IResult<T> Success(string message, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return new Result<T> { Succeeded = true, Message = message, StatusCode = httpStatusCode };
        }

        public static IResult<T> Success(T data, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return new Result<T> { Succeeded = true, Data = data, StatusCode = httpStatusCode };
        }

        public static IResult<T> Success(string message, T data, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return new Result<T> { Succeeded = true, Data = data, Message = message, StatusCode = httpStatusCode };
        }
        public T? Data { get; private init; }

        public T? Error { get; private init; }
    }
}
