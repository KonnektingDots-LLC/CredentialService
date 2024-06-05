
using Autofac;
using cred_system_back_end_app.Application.DTO.Notifications;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Emails;
using cred_system_back_end_app.Domain.Interfaces.pdf;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.AzureBlobStorage;
using cred_system_back_end_app.Infrastructure.Data;
using cred_system_back_end_app.Infrastructure.Data.Repositories;
using cred_system_back_end_app.Infrastructure.Logger;
using cred_system_back_end_app.Infrastructure.PdfReport.PDFServices;
using cred_system_back_end_app.Infrastructure.PdfReport.PDFServices.AttestationPDF;
using cred_system_back_end_app.Infrastructure.Smtp.Emails;

namespace cred_system_back_end_app.Infrastructure
{
    public class InfrastructureServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(LoggerService<>)).As(typeof(ILoggerService<>)).InstancePerLifetimeScope();

            // Repositories
            builder.RegisterGeneric(typeof(GenericAuditRepository<,>)).As(typeof(IGenericRepository<,>)).InstancePerLifetimeScope();

            builder.RegisterType<AttestationRepository>().As<IAttestationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ChangeLogRepository>().As<IChangeLogRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CredFormRepository>().As<ICredFormRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DelegateRepository>().As<IDelegateRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DocumentLocationRepository>().As<IDocumentLocationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<InsurerAdminRepository>().As<IInsurerAdminRepository>().InstancePerLifetimeScope();
            builder.RegisterType<InsurerCompanyRepository>().As<IInsurerCompanyRepository>().InstancePerLifetimeScope();
            builder.RegisterType<InsurerEmployeeRepository>().As<IInsurerEmployeeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<JsonProviderFormRepository>().As<IJsonProviderFormRepository>().InstancePerLifetimeScope();
            builder.RegisterType<NotificationProfileCompletionDetailRepository>().As<INotificationProfileCompletionDetailRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OcsAdminRepository>().As<IOcsAdminRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProviderCorporationRepository>().As<IProviderCorporationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProviderDelegatesRepository>().As<IProviderDelegatesRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProviderDetailRepository>().As<IProviderDetailRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProviderInsurerCompanyStatusRepository>().As<IProviderInsurerCompanyStatusRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProviderInsurerCompanyStatusHistoryRepository>().As<IProviderInsurerCompanyStatusHistoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProviderRepository>().As<IProviderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SpecialtyListRepository>().As<ISpecialtyListRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SubSpecialtyListRepository>().As<ISubSpecialtyListRepository>().InstancePerLifetimeScope();

            builder.RegisterType<Transaction>().As<ITransaction>().InstancePerLifetimeScope();

            // Notifications
            builder.RegisterType<DelegateStatusUpdateEmail>().As<IDelegateStatusUpdateEmail<NotificationEmailDto>>().InstancePerLifetimeScope();
            builder.RegisterType<ProviderDelegateStatusUpdateEmail>().As<IProviderDelegateStatusUpdateEmail<NotificationEmailDto>>().InstancePerLifetimeScope();
            builder.RegisterType<ProviderSubmitToInsurerNotificationEmail>().As<IProviderSubmitToInsurerNotificationEmail<NotificationEmailDto>>().InstancePerLifetimeScope();
            builder.RegisterType<InsurerToProviderStatusEmail>().As<IInsurerToProviderStatusEmail<NotificationEmailDto>>().InstancePerLifetimeScope();
            builder.RegisterType<ProviderSubmitToDelegateNotificationEmail>().As<IProviderSubmitToDelegateNotificationEmail<NotificationEmailDto>>().InstancePerLifetimeScope();
            builder.RegisterType<InsurerToProviderStatusRtpEmail>().As<IInsurerToProviderStatusRtpEmail<NotificationEmailDto>>().InstancePerLifetimeScope();
            builder.RegisterType<ProviderSubmitNotificationEmail>().As<IProviderSubmitNotificationEmail<NotificationEmailDto>>().InstancePerLifetimeScope();
            builder.RegisterType<InsurerInvitationNotificationEmail>().As<IInsurerInvitationNotificationEmail<NotificationEmailDto>>().InstancePerLifetimeScope();
            builder.RegisterType<DelegateInvitationNotificationEmail>().As<IDelegateInvitationNotificationEmail<NotificationEmailDto>>().InstancePerLifetimeScope();
            builder.RegisterType<ProfileCompletionNotificationEmail>().As<IProfileCompletionNotificationEmail<NotificationEmailDto>>().InstancePerLifetimeScope();
            builder.RegisterType<ProviderReviewNotificationEmail>().As<IProviderReviewNotificationEmail<NotificationEmailDto>>().InstancePerLifetimeScope();

            builder.RegisterType<FileStorageClient>().As<IFileStorageClient>().InstancePerLifetimeScope();
            builder.RegisterType<IIPCAPDFService>().As<IIipcaPdfService>().InstancePerLifetimeScope();
            builder.RegisterType<AttestationPDFService>().As<IAttestationPdfService>().InstancePerLifetimeScope();
        }
    }
}
