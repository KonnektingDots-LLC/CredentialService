namespace cred_system_back_end_app.Domain.Interfaces.Emails
{
    public interface IDelegateStatusUpdateEmail<T> where T : class
    {
        Task<(string, string)> SendEmailAsync(T ToEmailDto);
    }
}
