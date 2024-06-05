namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class AppNotificationTypeListEntity : RecordHistoryTypeList
    {
        public int Id { get; set; }

        public int AppNotificationTypeName { get; set; }

        #region related entities

        public ICollection<AppNotificationEntity> AppNotifications { get; set; }

        #endregion
    }
}
