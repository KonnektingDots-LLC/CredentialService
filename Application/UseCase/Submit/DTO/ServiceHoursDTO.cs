namespace cred_system_back_end_app.Application.UseCase.Submit.DTO
{
    public class ServiceHoursDTO
    {
        public DailyServiceHoursDTO Monday { get; set; }
        public DailyServiceHoursDTO Tuesday { get; set; }
        public DailyServiceHoursDTO Wednesday { get; set; }
        public DailyServiceHoursDTO Thursday { get; set; }
        public DailyServiceHoursDTO Friday { get; set; }
        public DailyServiceHoursDTO Saturday { get; set; }
        public DailyServiceHoursDTO Sunday { get; set; }
    }
}
