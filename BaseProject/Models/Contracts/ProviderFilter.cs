namespace BaseProject.Models.Contracts;

public class ProviderFilter : PaginationRequest
{
    public string? Search { get; set; }
    public long? LastSync { get; set; }
}
