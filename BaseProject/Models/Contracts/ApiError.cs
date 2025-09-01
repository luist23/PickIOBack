namespace BaseProject.Models.Contracts;

public class ApiError
{
    public string Message { get; }
    public string? Name { get; }
    public ApiCodes.ErrorCode Code { get; }

    public ApiError(string message)
    {
        Message = message;
        Code = ApiCodes.ErrorCode.BadRequest;
    }

    public ApiError(string message, ApiCodes.ErrorCode code)
    {
        Message = message;
        Code = code;
    }

    public ApiError(string message, string? name, ApiCodes.ErrorCode code)
    {
        Message = message;
        Name = name;
        Code = code;
    }
}