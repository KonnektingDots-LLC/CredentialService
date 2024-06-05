using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class MedicalSchoolEntity : ListMemberEntityBase
    {
        public int Id { get; set; }

        public int ProviderId { get; set; }

        public string Name { get; set; }

        public int AddressId { get; set; }

        public int GraduationMonth { get; set; }

        public int GraduationYear { get; set; }

        public string MSSpecialtyDegreeRecieved { get; set; }

        public string MSSpecialty { get; set; }

        public DateTime SpecialtyCompletionDate { get; set; }

        public string? SpecialtyDegree { get; set; }


        #region related entities
        public ProviderEntity Provider { get; set; }

        public AddressEntity Address { get; set; }
        public MedicalSchoolDocumentEntity MedicalSchoolDocument { get; set; }
        #endregion
    }
}
