namespace BaseProject.Configuration;

public static class Routes
{
    private const string Api = "api";

    private const string Auth = "auth";
    private const string Barcode = "barcode";

    public const string AuthApiRoute = $"{Api}/{Auth}";
    public const string BarcodeApiRoute = $"{Api}/{Barcode}";
    public const string BranchOfficeApiRoute = $"{Api}/branch-office";
    public const string CustomerApiRoute = $"{Api}/customer";
    public const string JustificationApiRoute = $"{Api}/justification";
    public const string ProductApiRoute = $"{Api}/product";
}