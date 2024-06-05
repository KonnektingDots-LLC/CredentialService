using cred_system_back_end_app.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Domain.Entities
{
    public class OrganizationTypeEntity : EntityHistoryTypeList
    {
        public int Id { get; set; }
        [MaxLength(30)]

        public string Name { get; set; }


        #region dependent entities

        public ICollection<SpecialtyListEntity> Specialty { get; set; }

        public ICollection<SubSpecialtyListEntity> SubSpecialty { get; set; }

        //public ICollection<OrganizationAddressEntity> OrganizationAddress { get; set; }

        #endregion
    }
}
