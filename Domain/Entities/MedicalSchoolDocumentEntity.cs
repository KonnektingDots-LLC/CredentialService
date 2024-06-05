using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class MedicalSchoolDocumentEntity : EntityCommon
    {
        public int MedicalSchoolId { get; set; }
        public string AzureBlobFilename { get; set; }

        #region relationships
        public MedicalSchoolEntity MedicalSchool { get; set; }
        public DocumentLocationEntity DocumentLocation { get; set; }
        #endregion
    }
}
