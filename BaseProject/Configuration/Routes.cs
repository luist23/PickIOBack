namespace BaseProject.Configuration;

public static class Routes
{
    private const string Api = "api";

    private const string Auth = "auth";
    private const string Barcode = "auth";

    public const string AuthApiRoute = $"{Api}/{Auth}";
    public const string BarcodeApiRoute = $"{Api}/{Barcode}";
}