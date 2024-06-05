namespace cred_system_back_end_app.Application.UseCase.Submit.DTO
{
    public class FileBaseDTO
    {
        public string? AzureBlobFilename { get; set; }
        public string Name { get; set; }
        public int DocumentTypeId { get; set; }
    }
}