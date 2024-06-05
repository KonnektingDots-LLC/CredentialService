using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class DocumentLocationEntity
    {
        [Key]
        public string AzureBlobFilename { get; set; }
        public int ProviderId { get; set; }
        public ProviderEntity Provider { get; set; }
        public string UploadFilename { get; set; }
        public string Extension { get; set; }
        public string ContainerName { get; set; }
        public string Uri { get; set; }
        public long SizeInBytes { get; set; }
        public string? UploadBy { get; set; }

        public DateTime? UploadDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? LetterDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? CertificateDate { get; set; }

        public string? NPI { get; set; }

        public string? OldFilename { get; set; }


        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; } = true;

        /* EF Relation */
     
        public int DocumentTypeId { get; set; }
        public DocumentTypeEntity DocumentType { get; set; }

        public List<DocumentCommentEntity> DocumentComment { get; set; }

        public ProviderSpecialtyEntity ProviderSpecialty { get; set; }
        public ProviderSubSpecialtyEntity ProviderSubSpecialty { get; set; }

        public CorporationDocumentEntity CorporationDocument { get; set; }

        public BoardDocumentEntity BoardDocument { get; set; }
        public EducationInfoDocumentEntity EducationInfoDocument { get; set; }
        public MedicalSchoolDocumentEntity MedicalSchoolDocument { get; set; }

    }
}
