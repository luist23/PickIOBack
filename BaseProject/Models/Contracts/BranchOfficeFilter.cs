namespace BaseProject.Models.Contracts;

public class BranchOfficeFilter : PaginationRequest
{
    public string? Search { get; set; }
    public long? LastSync { get; set; }
}
