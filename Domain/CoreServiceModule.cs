using Autofac;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Services;

namespace cred_system_back_end_app.Domain
{
    public class CoreServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProviderService>().As<IProviderService>().InstancePerLifetimeScope();
            builder.RegisterType<InsurerService>().As<IInsurerService>().InstancePerLifetimeScope();
            builder.RegisterType<CrendentialingFormService>().As<ICrendentialingFormService>().InstancePerLifetimeScope();
            builder.RegisterType<DelegateService>().As<IDelegateService>().InstancePerLifetimeScope();
            builder.RegisterType<NotificationService>().As<INotificationService>().InstancePerLifetimeScope();
            builder.RegisterType<ProviderInsurerCompanyStatusService>().As<IProviderInsurerCompanyStatusService>().InstancePerLifetimeScope();
            builder.RegisterType<MultiFileUploadService>().As<IMultiFileUploadService>().InstancePerLifetimeScope();
        }
    }
}
