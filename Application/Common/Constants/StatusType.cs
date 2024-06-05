namespace cred_system_back_end_app.Application.Common.Constants
{
    public class StatusType
    {
        //Provider and Insurer
        public const string APPROVED = "APPROVED";
        public const string PENDING = "PENDING";
        public const string REJECTED = "REJECTED";
        public const string RETURNED_TO_PROVIDER = "RTP";

        //Only Provider
        public const string DRAFT = "DRAFT";
        public const string SUBMITTED = "SUBMITTED";
        public const string RESUBMITTED = "RESUBMIT";

    }
}
