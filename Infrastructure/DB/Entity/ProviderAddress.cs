namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class ProviderAddressEntity : ListMemberEntityBase
    {
        public int ProviderId { get; set; }
        public int AddressId { get; set; }

        public string AddressMedicaidID { get; set; }
        public int AddressPrincipalTypeId { get; set; }
        public bool IsAcceptingNewPatient { get; set; }
        public bool IsComplyWithAda { get; set; }
        public string AdaComplyComment { get; set; }
        public bool IsMovedMoreThan5Miles { get; set; }
        public bool IsAdaptedToDiabledPatient { get; set; }

        #region helpers

        public  AddressEntity Address { get; set; }
        public AddressPrincipalTypeEntity AddressPrincipalType { get; set; }

        #endregion
    }
}
