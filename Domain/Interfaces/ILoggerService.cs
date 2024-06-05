namespace cred_system_back_end_app.Domain.Interfaces
{
    public interface ILoggerService<T> where T : class
    {
        void Info(string message);
        void Error(string message, Exception? exception);
        void Warn(string message);
        public string? GetTransactionId();
    }
}
