using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class EducationInfoDocumentEntity : EntityCommon
    {
        public int EducationInfoId { get; set; }
        public string AzureBlobFilename { get; set; }

        #region relationships
        public EducationInfoEntity EducationInfo { get; set; }
        public DocumentLocationEntity DocumentLocation { get; set; }
        #endregion
    }
}
