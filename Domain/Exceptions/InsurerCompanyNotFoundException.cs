namespace cred_system_back_end_app.Domain.Exceptions
{
    [Serializable]
    internal class InsurerCompanyNotFoundException : GenericInsurerException
    {

        public InsurerCompanyNotFoundException(string? message) : base(message)
        {
        }
    }
}