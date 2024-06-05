using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class AppNotificationEntity : EntityCommon
    {
        public int Id { get; set; }

        public int AppNotificationTypeId { get; set; }

        public DateTime? SentDateTime { get; set; }

        public string NotificationFrom { get; set; }

        public string NotificationTo { get; set; }

        #region related entities

        public AppNotificationTypeListEntity AppNotificationType { get; set; }

        #endregion
    }
}
