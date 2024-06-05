namespace cred_system_back_end_app.Domain.Services.Submit.DTO.EducationDTOs
{
    public class BoardCertificateDTO
    {
        /// <summary>
        /// List of specialty id's in SepcialtyTypeList
        /// </summary>
        // TODO: Create BoardSpecialty M2M Table
        public string PublicId { get; set; }
        public int[] SpecialtyBoard { get; set; }
        public FileBaseDTO CertificateFile { get; set; }
        public string IssuedDate { get; set; }
        public string ExpirationDate { get; set; }
    }
}
