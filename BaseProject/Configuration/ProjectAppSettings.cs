namespace BaseProject.Configuration;

public partial class ProjectAppSettings
{
    public ConnectionAppSettings ConnectionStrings { get; set; } = new();
    public string TokenKey { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
    public JwtAppSettings Jwt { get; set; } = new();
}

public class ConnectionAppSettings
{
    public string TypeConnection { get; set; } = "SQLite";
    public string SqlServer { get; set; } = string.Empty;
    public string MySql { get; set; } = string.Empty;
    public string SqLite { get; set; } = "Data Source=SQLite.db";
}

public class JwtAppSettings
{
    public string Key { set; get; } = "23-SuperClaveSecretaQueDebeSerLargaYSegura123!";
    public string Issuer { set; get; } = string.Empty;
    public string Audience { set; get; } = string.Empty;
    public int SessionExpirationMinutes { set; get; } = 30;
    public int TokenExpirationMinutes { set; get; } = 30;
}