namespace cred_system_back_end_app.Application.Common.Services
{
    public class FireAndForgetService
    {
        private IServiceScopeFactory _serviceScopeFactory;

        public FireAndForgetService(IServiceScopeFactory serviceScopeFactory) 
        { 
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Execute<TService>(Func<TService, Task> action)
        {
            Task.Run(async () =>
            {
                try
                {
                    using var serviceScope = _serviceScopeFactory.CreateScope();

                    var service = serviceScope.ServiceProvider.GetService<TService>();

                    await action(service);
                }
                catch
                {
                    // intentionally left blank, if an exception is raised we want it to be ignored.
                }
            });
        }
    }
}
