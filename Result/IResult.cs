using FluentValidation.Results;
using System.Net;
using System.Text.Json.Serialization;

namespace Mbis.Cognito.Result
{
    public interface IResult
    {
        [JsonInclude]
        HttpStatusCode StatusCode { get; set; }

        [JsonInclude]
        bool Failed { get; }

        [JsonInclude]
        string Message { get; set; }

        [JsonInclude]
        bool Succeeded { get; set; }
        [JsonInclude]
        public List<ValidationFailure> Errors { get; set; }
    }

    public interface IResult<out T> : IResult
    {
        T? Data { get; }
        T? Error { get; }
    }
}
