namespace BaseProject.Models.Contracts;

#pragma warning disable CA1008
public abstract class ApiCodes
{
    public enum SuccessCode
    {
        Ok = 200,
        Created = 201,
        NoContent = 204,
    }

    public enum ErrorCode
    {
        BadRequest = 400,
        UnAuthorized = 401,
        ForBidden = 403,
        Conflict = 409,
        NotImplemented = 501,
    }
}
#pragma warning restore CA1008