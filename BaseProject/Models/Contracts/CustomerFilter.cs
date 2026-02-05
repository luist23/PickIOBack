namespace BaseProject.Models.Contracts;

public class CustomerFilter : PaginationRequest
{
    public string? Search { get; set; }
    public long? LastSync { get; set; }
}
