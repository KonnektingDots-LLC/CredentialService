namespace cred_system_back_end_app.Application.Common.Helpers
{
    public static class QueryableHelper
    {
        public static IQueryable<T> Paginated<T>(this IQueryable<T> queryable, int offset, int limitPerPage) 
        { 
            return queryable
                .Skip(offset)
                .Take(limitPerPage);
        }
    }
}
