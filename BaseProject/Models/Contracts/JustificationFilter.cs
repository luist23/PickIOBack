namespace BaseProject.Models.Contracts;

public class JustificationFilter : PaginationRequest
{
    public string? Search { get; set; }
    public long? LastSync { get; set; }
}
