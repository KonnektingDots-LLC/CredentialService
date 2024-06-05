namespace cred_system_back_end_app.Application.Common.Helpers
{
    public static class PaginationHelper
    {
        public static double GetTotalNumberOfPages(int limitPerPage, int recordCount)
        {
            return Math.Ceiling((double)recordCount / limitPerPage);
        }

        public static int GetOffset(int currentPage, int limit)
        {
            if (currentPage < 0)
            {
                throw new AggregateException("currentPage must be >= 1");
            }

            return (currentPage - 1) * limit;
        }
    }
}
