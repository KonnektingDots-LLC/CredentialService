namespace cred_system_back_end_app.Infrastructure.DB
{
    public class RecordHistoryTypeList : RecordHistory
    {
        public bool IsExpired { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }
}
