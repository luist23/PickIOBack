using System.Collections.ObjectModel;
using BaseProject.Models.Contracts;
using BaseProject.Models.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers;

public static class ProjectController
{
    public static JsonResult Respond<T>(T value, ApiCodes.SuccessCode code = ApiCodes.SuccessCode.Ok) where T : class
    {
        return new JsonResult(new ApiResponse<T>(value))
        {
            StatusCode = (int)code,
        };
    }

    public static JsonResult RespondPagination<T>(IOrderedQueryable<T> list, PaginationRequest paginationRequest,
        ApiCodes.SuccessCode code = ApiCodes.SuccessCode.Ok) where T : class
    {
        var meta = new Metadata(paginationRequest, list.Count());
        var value = new ApiPaginationResponse<List<T>>(
            Paginate(meta, list),
            meta
        );
        return RespondPagination(value, code);
    }

    public static JsonResult RespondPagination<T>(IQueryable<T> list, PaginationRequest paginationRequest,
        ApiCodes.SuccessCode code = ApiCodes.SuccessCode.Ok) where T : class
    {
        var meta = new Metadata(paginationRequest, list.Count());
        var value = new ApiPaginationResponse<List<T>>(
            Paginate(meta, list),
            meta
        );
        return RespondPagination(value, code);
    }

    public static JsonResult RespondPagination<T>(ApiPaginationResponse<T> value,
        ApiCodes.SuccessCode code = ApiCodes.SuccessCode.Ok) where T : class
    {
        return new JsonResult(value)
        {
            StatusCode = (int)code,
        };
    }

    public static JsonResult Reject(string message, ApiCodes.ErrorCode code = ApiCodes.ErrorCode.BadRequest)
    {
        return new JsonResult(new ApiFailure(message, code))
        {
            StatusCode = (int)code
        };
    }

    public static JsonResult Reject(
        Collection<string> messages,
        ApiCodes.ErrorCode code = ApiCodes.ErrorCode.BadRequest
    )
    {
        var res = messages.Select(e => new ApiError(e, code));
        return Reject(res, code);
    }

    public static JsonResult Reject(ApiError error)
    {
        return new JsonResult(new ApiFailure(error))
        {
            StatusCode = (int)error.Code
        };
    }

    public static JsonResult Reject(
        string message, string model,
        ApiCodes.ErrorCode code = ApiCodes.ErrorCode.BadRequest
    )
    {
        return new(new ApiFailure(message, model, code))
        {
            StatusCode = (int)code
        };
    }

    public static JsonResult Reject(IEnumerable<ApiError> errors, ApiCodes.ErrorCode code)
    {
        return new(new ApiFailure(errors))
        {
            StatusCode = (int)code
        };
    }

    public static IQueryable Paginate<T>(Metadata request, IOrderedQueryable<T> list)
    {
        if (request.Size == 0)
        {
            return list;
        }

        var pagination = list
            .Skip(request.GetSkip())
            .Take(request.Size);
        return pagination;
    }

    public static IQueryable Paginate<T>(Metadata request, IQueryable<T> list)
    {
        if (request.Size == 0)
        {
            return list;
        }

        var pagination = list
            .Skip(request.GetSkip())
            .Take(request.Size);
        return pagination;
    }

    public static JsonResult JsonResponse<T>(
        ResultResponse response,
        ApiCodes.ErrorCode code = ApiCodes.ErrorCode.BadRequest
    ) where T : class
    {
        return response switch
        {
            ResultResponse.Errors errors => Reject(errors.Result, code),
            ResultResponse.Error error => Reject(error.Result, code),
            ResultResponse.ErrorApi errorApi => Reject(errorApi.Result),
            ResultResponse.Success<T> success => Respond(success.Result),
            _ => Reject("Respuesta no esperada", ApiCodes.ErrorCode.NotImplemented),
        };
    }
}