using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Models.Responses;

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

    public class Errors(List<string> result) : ResultResponse
    {
        public List<string> Result = result;
    }

    public static JsonResult JsonResponse(ResultResponse response, int codeError = 400)
    {
        return response switch
        {
            Errors errors => new JsonResult(new { Errors = errors.Result })
            {
                StatusCode = codeError
            },
            Error error => new JsonResult(new { Error = error.Result })
            {
                StatusCode = codeError
            },
            Success<string> success => new JsonResult(success.Result)
            {
                StatusCode = 200
            },
            _ => new JsonResult(new { Error = "Respuesta no esperada" })
            {
                StatusCode = (int)HttpStatusCode.NotImplemented
            },
        };
    }

    public static JsonResult ErrorResponse(string error, int codeError = 400)
    {
        return new JsonResult(new { Error = error })
        {
            StatusCode = codeError
        };
    }
}