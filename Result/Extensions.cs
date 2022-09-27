using System.Net;

namespace Mbis.Cognito.Result
{
    public static class Extensions
    {
        public static IResult<T> Fail<T>(this T errordata)
        {
            return Result<T>.Fail(errordata);
        }

        public static IResult<T> Fail<T>(this T errordata, string message,
            HttpStatusCode statusCode = HttpStatusCode.UnprocessableEntity)
        {
            return Result<T>.Fail(message, errordata, statusCode);
        }

        public static IResult<T> Success<T>(this T data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return Result<T>.Success(data, statusCode);
        }

        public static IResult<T> Success<T>(this T data, string message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return Result<T>.Success(message, data, statusCode);
        }
    }
}
