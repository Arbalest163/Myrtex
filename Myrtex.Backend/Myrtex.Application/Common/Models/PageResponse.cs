namespace Myrtex.Application.Common.Models;

public sealed class PageResponse<TItem>
{
    public int Page { get; set; }

    public int PageSize { get; set; }

    public int TotalPages { get; set; }

    public int TotalItems { get; set; }

    public TItem[] Items { get; set; }
}
