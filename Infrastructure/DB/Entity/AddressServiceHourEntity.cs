
namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class AddressServiceHourEntity:RecordHistory
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public string? DayOfWeek { get; set; }
        public TimeSpan HourFrom { get; set; }
        public TimeSpan HourTo { get; set; }
        public bool? IsClosed { get; set; }
        public string? Comment { get; set; }

        #region relationships

        public AddressEntity Address { get; set; }

        #endregion
    }
}
