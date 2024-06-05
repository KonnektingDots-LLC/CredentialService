namespace cred_system_back_end_app.Application.DTO.Responses
{
    public class AddressResponseDto
    {
        public int Id { get; set; }

        public string? AddressName { get; set; }

        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public string? AddressCity { get; set; }

        public string? AddressState { get; set; }

        public string? AddressZipCode { get; set; }

        public string? AddressCounty { get; set; }
    }
}
