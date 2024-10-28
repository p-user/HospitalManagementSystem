namespace Shared.Pagination
{
    public class PaginatedResult<TEntity>
        (int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        where TEntity : class
    {
        public int PageIndex { get; } = pageIndex;
        public int PageSize { get; } = pageSize;
        public int Count { get; } = (int)count;
        public IEnumerable<TEntity> Data { get; } = data;
    }
}
