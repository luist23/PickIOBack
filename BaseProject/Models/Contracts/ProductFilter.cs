namespace BaseProject.Models.Contracts;

public class ProductFilter : PaginationRequest
{
    public string? Search { get; set; }
    public long? LastSync { get; set; }
}
