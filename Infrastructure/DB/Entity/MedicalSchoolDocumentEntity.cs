namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class MedicalSchoolDocumentEntity : RecordHistory
    {
        public int MedicalSchoolId { get; set; }
        public string AzureBlobFilename { get; set; }

        #region relationships
        public MedicalSchoolEntity MedicalSchool { get; set; }
        public DocumentLocationEntity DocumentLocation { get; set; }
        #endregion
    }
}
