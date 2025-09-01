namespace BaseProject.Models.Contracts;

public class ApiResponse<T>(T value) where T : class
{
    public T Data { get; } = value;
}

public class ApiFailure
{
    public IEnumerable<ApiError> Errors { get; }

    public ApiFailure(ApiError error)
    {
        Errors = new[] { error };
    }

    public ApiFailure(string message, ApiCodes.ErrorCode code)
    {
        Errors = new[] { new ApiError(message, code) };
    }

    public ApiFailure(string message, string model, ApiCodes.ErrorCode code)
    {
        Errors = new[] { new ApiError(message, model, code) };
    }
    public ApiFailure(IEnumerable<ApiError> errors)
    {
        Errors = errors;
    }
}

public class ApiPaginationResponse<T>(IQueryable value, Metadata meta) : ApiResponse<IQueryable>(value)
    where T : class
{
    public Metadata Meta { get; } = meta;
}