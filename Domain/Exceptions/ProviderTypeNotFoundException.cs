namespace cred_system_back_end_app.Domain.Exceptions
{
    [Serializable]
    public class ProviderTypeNotFoundException : Exception
    {
        public ProviderTypeNotFoundException()
        {
        }

        public ProviderTypeNotFoundException(string? message) : base(message)
        {
        }
    }
}