using System.Linq.Expressions;

namespace EdTech.Quiz.Application.Helpers;

public class PaginatedResult<T>
{
    public List<T>? Items { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }

}
public class PaginationHelper
{
    public static PaginatedResult<T> Paginate<T, TKey>(
        IQueryable<T> source,
        int pageNumber,
        int pageSize,
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, TKey>>? order = null,
        bool orderByDescending = false
    )
    {

        if (filter != null)
        {
            source = source.Where(filter);
        }
        int count = source.Count();

        if (order != null)
        {
            source = orderByDescending ? source.OrderByDescending(order) : source.OrderBy(order);
        }

        List<T> items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new()
        {
            Items = items,
            PageNumber = pageNumber,
            TotalCount = count,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        };

    }
}
