using Azure.Storage.Blobs;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Services.DTO;

namespace cred_system_back_end_app.Domain.Services.Helpers
{
    public class DocumentLocationHelper
    {
        public static DocumentLocationEntity GetDocumentLocationEntity(string newFileName, MultiFileUploadDto file,
            string containerName, BlobClient client, DateTime uploadDate, int ProviderId)
        {
            return new DocumentLocationEntity
            {
                AzureBlobFilename = newFileName,
                ProviderId = ProviderId,
                UploadFilename = file.File.FileName,
                Extension = Path.GetExtension(newFileName).ToLower().Substring(1),
                ContainerName = containerName,
                Uri = client.Uri.AbsoluteUri,
                SizeInBytes = file.File.Length,
                UploadBy = "lleon@wovenware",//B2C
                UploadDate = uploadDate,
                ExpirationDate = file.ExpirationDate,
                IssueDate = file.IssueDate,
                CertificateDate = file.CertificateDate,
                LetterDate = file.LetterDate,
                DocumentTypeId = file.DocumentTypeId,
                NPI = file.NPI,
                OldFilename = file.OldFilename
            };
        }
    }
}
