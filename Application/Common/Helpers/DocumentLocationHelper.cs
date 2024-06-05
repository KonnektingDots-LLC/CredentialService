using Azure.Storage.Blobs;
using cred_system_back_end_app.Application.UseCase.Submit.DTO;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.FileSystem.MultiFileUpload.DTO;
using System.ComponentModel;

namespace cred_system_back_end_app.Application.Common.Helpers
{
    public class DocumentLocationHelper
    {
        public static DocumentLocationEntity GetDocumentLocationEntity(string newFileName, MultiFileUploadDto file, 
            BlobContainerClient container, BlobClient client,DateTime uploadDate, int ProviderId)
        {
            return new DocumentLocationEntity
            {
                AzureBlobFilename = newFileName,
                ProviderId = ProviderId,
                UploadFilename = file.File.FileName,
                Extension = Path.GetExtension(newFileName).ToLower().Substring(1),
                ContainerName = container.Name,
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
