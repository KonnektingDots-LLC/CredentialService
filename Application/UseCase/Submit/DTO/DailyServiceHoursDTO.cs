using System.Reflection.Metadata.Ecma335;

namespace cred_system_back_end_app.Application.UseCase.Submit.DTO
{
    public class DailyServiceHoursDTO
    {
        public int DayOfWeek { get; set; }
        public string HourFrom { get; set; }
        public string HourTo { get; set; }
        public bool IsClosed { get; set; }
    }
}
