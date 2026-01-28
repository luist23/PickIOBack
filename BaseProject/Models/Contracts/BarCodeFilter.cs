using System.Globalization;

namespace BaseProject.Models.Contracts;

public class BarCodeFilter : PaginationRequest
{
    public string? Search { get; set; }
    public long? LastSync { get; set; }
}
