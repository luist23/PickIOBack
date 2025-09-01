using System.Collections.ObjectModel;

namespace BaseProject.Models.Contracts.Responses;

public class ResultResponse
{
    public class Success<T>(T result) : ResultResponse
    {
        public T Result = result;
    }

    public class Error(string result) : ResultResponse
    {
        public string Result = result;
    }

    public class ErrorApi(string error, string? name = null, ApiCodes.ErrorCode code = ApiCodes.ErrorCode.BadRequest)
        : ResultResponse
    {
        public ApiError Result = new ApiError(error, name, code);
    }

    public class Errors(Collection<string> result) : ResultResponse
    {
        public readonly Collection<string> Result = result;
    }
}