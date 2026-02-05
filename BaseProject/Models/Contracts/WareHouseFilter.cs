namespace BaseProject.Models.Contracts;

public class WareHouseFilter : PaginationRequest
{
    public string? Search { get; set; }
    public long? LastSync { get; set; }
}
