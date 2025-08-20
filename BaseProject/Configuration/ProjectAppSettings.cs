namespace BaseProject.Configuration;

public partial class ProjectAppSettings
{
    public ConnectionAppSettings ConnectionStrings { get; set; } = new ConnectionAppSettings();
    public string TokenKey { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;

}

public class ConnectionAppSettings
{
    public string TypeConnection { get; set; } = "SQLite";
    public string SQLServer { get; set; } = string.Empty;
    public string MySQL { get; set; } = string.Empty;
    public string SQLite { get; set; } = "Data Source=SQLite.db";
}