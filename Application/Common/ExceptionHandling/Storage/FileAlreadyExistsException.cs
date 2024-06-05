namespace cred_system_back_end_app.Application.Common.ExceptionHandling.Storage
{
    /// <summary>
    /// An exception to be thrown by implementations of <see
    /// cref="Services.Storage.IStorageService"/> whenever an Upload operation is requested against
    /// a file that already exists.
    /// </summary>
    [Serializable]
    public class FileAlreadyExistsException
    {

    }
}
