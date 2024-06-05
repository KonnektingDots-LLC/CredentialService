using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class AppNotificationTypeListEntity : EntityHistoryTypeList
    {
        public int Id { get; set; }

        public int AppNotificationTypeName { get; set; }

        #region related entities

        public ICollection<AppNotificationEntity> AppNotifications { get; set; }

        #endregion
    }
}
