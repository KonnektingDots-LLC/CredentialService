using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.CRUD.MedicalGroup.DTO;
using cred_system_back_end_app.Application.CRUD.Provider.DTO;
using cred_system_back_end_app.Application.UseCase.Notifications.DTO;
using cred_system_back_end_app.Infrastructure.B2C;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.UseCase.Notifications
{
    public class SaveNotificationCase
    {
        private readonly DbContextEntity _contextEntity;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly GetB2CInfo _getB2CInfo;
        public SaveNotificationCase(DbContextEntity contextEntity,
            IConfiguration configuration, 
            IMapper mapper, 
            GetB2CInfo getB2CInfo) 
        {
            _contextEntity = contextEntity;
            _configuration = configuration;
            _mapper = mapper;
            _getB2CInfo = getB2CInfo;
        }

        public NotificationEntity CreateNotification(NotificationEntity newNotification)
        {
            var notificationTypeFound = _contextEntity.NotificationType.Where(nt => nt.Id == newNotification.NotificationTypeId).FirstOrDefault();

            if (notificationTypeFound == null)
            {
                throw new EntityNotFoundException();
            }
            //TODO: Validate Resource Id segun el NotificationType para saber que exista

            //var notificationEntity = _mapper.Map<NotificationEntity>(newNotification);
            newNotification.CreatedBy = _getB2CInfo.Email;              

            _contextEntity.Notification.AddRange(newNotification);

            return newNotification;
        }

        public NotificationStatusEntity CreateNotificationStatus(NotificationStatusEntity newNotificationStatus)
        {

            //var notificationStatusEntity = _mapper.Map<NotificationStatusEntity>(newNotificationStatus);
            newNotificationStatus.CreatedBy = _getB2CInfo.Email;               

                _contextEntity.NotificationStatus.Add(newNotificationStatus);
                           
                return newNotificationStatus;

        }

        public NotificationErrorEntity CreateNotificationError(NotificationErrorEntity newNotificationError)
        {
            var notificationStatusFound = _contextEntity.NotificationStatus.Where(nt => nt.Id == newNotificationError.NotificationStatusId).FirstOrDefault();

            if (notificationStatusFound == null)
            {
                throw new EntityNotFoundException();
            }


            //var notificationErrorEntity = _mapper.Map<NotificationErrorEntity>(newNotificationError);
            newNotificationError.CreatedBy = _getB2CInfo.Email;

            _contextEntity.NotificationError.AddRange(newNotificationError);

            return newNotificationError;
        }

        public void CommitNotification()
        {
            using (var dbTransaction = _contextEntity.Database.BeginTransaction())
            {
                _contextEntity.SaveChanges();
                dbTransaction.Commit();
            }           
        }

        public async Task SaveNotificationEntities(NotificationRequestDto notification )
        {

            if (notification.CreateNotificationError != null)
            {
                
            }
            
        }

        public async Task SaveNotification(NotificationEntity notificationEntity)
        {
            await _contextEntity.SaveAsTransactionAsync(notificationEntity);
        }
    }
}
