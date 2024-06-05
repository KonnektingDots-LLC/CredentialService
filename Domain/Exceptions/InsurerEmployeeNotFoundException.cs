namespace cred_system_back_end_app.Domain.Exceptions
{
    [Serializable]
    internal class InsurerEmployeeNotFoundException : GenericInsurerException
    {

        public InsurerEmployeeNotFoundException(string? message) : base(message)
        {
        }
    }
}