namespace cred_system_back_end_app.Domain.Entities
{
    public class AddressServHourPlanAccept
    {
        public int Id { get; set; }
        public int AddressServiceHourId { get; set; }

        public int AddressPlanAcceptListId { get; set; }
    }
}
