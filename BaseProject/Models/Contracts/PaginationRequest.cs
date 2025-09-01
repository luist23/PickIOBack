namespace BaseProject.Models.Contracts;

public abstract class PaginationRequest
{
    private int _size = 10;
    private int _page = 1;

    public int Size
    {
        get => _size;
        set => _size = SizeValidation(value);
    }

    public int Page
    {
        get => _page;
        set => _page = PageValidation(value);
    }

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

    // public int getPosition()
    // {
    //     return Size * Page;
    // }
    //
    // public int getSkip()
    // {
    //     return Size * (Page - 1);
    // }
}