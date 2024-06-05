using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Services.Submit.DTO;
using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Application.Common.Helpers
{
    public static class ServiceHoursHelper
    {
        public static ICollection<AddressServiceHourEntity> GetAddressServiceHourEntities(DailyServiceHoursDTO[] serviceHoursDTO)
        {
            return serviceHoursDTO
                .Select(serviceHour => HandleDailyServiceHours(serviceHour))
                .ToArray();
        }

        public static string GetDayOfWeek(this DailyServiceHoursDTO dailyServiceHoursDTO)
        {
            switch (dailyServiceHoursDTO.DayOfWeek)
            {
                case 0:
                    return "Monday";

                case 1:
                    return "Tuesday";

                case 2:
                    return "Wednesday";

                case 3:
                    return "Thursday";

                case 4:
                    return "Friday";

                case 5:
                    return "Saturday";

                case 6:
                    return "Sunday";

                default:
                    return "Sunday";
            }
        }

        public static string GetFormattedServiceHoursString(this ICollection<AddressServiceHourEntity> serviceHours)
        {
            var openServiceHours = serviceHours.Where(x => !(bool)x.IsClosed);

            var openServiceHoursString = openServiceHours
                .Aggregate("", (acc, serviceHour) => $"{acc}\n{GetServiceHours(serviceHour)}");

            return openServiceHoursString;
        }

        public static AddressServiceHourEntity HandleDailyServiceHours(DailyServiceHoursDTO serviceHour)
        {
            if (IsClosedOrUnavailable(serviceHour))
            {
                return new AddressServiceHourEntity
                {
                    DayOfWeek = serviceHour.GetDayOfWeek(),
                    IsClosed = serviceHour.IsClosed
                };
            }

            return new AddressServiceHourEntity
            {
                DayOfWeek = serviceHour.GetDayOfWeek(),
                HourFrom = TimeSpan.Parse(serviceHour.HourFrom),
                HourTo = TimeSpan.Parse(serviceHour.HourTo),
                IsClosed = serviceHour.IsClosed
            };
        }

        private static bool IsClosedOrUnavailable(DailyServiceHoursDTO serviceHour)
        {
            return serviceHour.IsClosed || serviceHour.HourFrom.IsNullOrEmpty() || serviceHour.HourTo.IsNullOrEmpty();
        }

        private static string GetServiceHours(AddressServiceHourEntity serviceHour)
        {
            var hourFrom = DateTime.Parse(serviceHour.HourFrom.ToString());
            var hourTo = DateTime.Parse(serviceHour.HourTo.ToString());

            return $"{serviceHour.DayOfWeek}: {hourFrom.ToString(@"hh\:mm tt")} - {hourTo.ToString(@"hh\:mm tt")}";
        }
    }
}
