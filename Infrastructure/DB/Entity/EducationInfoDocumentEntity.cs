namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class EducationInfoDocumentEntity : RecordHistory
    {
        public int EducationInfoId { get; set; }
        public string AzureBlobFilename { get; set; }

        #region relationships
        public EducationInfoEntity EducationInfo { get; set; }
        public DocumentLocationEntity DocumentLocation { get; set; }
        #endregion
    }
}
