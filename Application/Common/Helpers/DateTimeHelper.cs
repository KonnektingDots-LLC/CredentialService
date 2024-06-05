using cred_system_back_end_app.Application.Common.ExceptionHandling;

namespace cred_system_back_end_app.Application.Common.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime ParseDate(string date)
        {
            DateTime parsedDate;

            if (!DateTime.TryParse(date, out parsedDate))
            {
                throw new DateTimeParsingErrorException($"Error parsing {nameof(date)}");
            }

            return parsedDate;
        }
    }
}
