namespace BaseProject.Models.Contracts;

public abstract class PaginationRequest
{
    public int Size
    {
        get;
        init => field = SizeValidation(value);
    } = 10;

    public int Page
    {
        get;
        init => field = PageValidation(value);
    } = 1;

    private static int SizeValidation(int size)
    {
        return size < 0 ? 100 : size;
    }

    private static int PageValidation(int size)
    {
        return size <= 0 ? 1 : size;
    }

    public void Deconstruct(out int size, out int page)
    {
        size = Size;
        page = Page;
    }
}