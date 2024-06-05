using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Application.Common.ResponseTO
{
    public class ProviderByDelegateResponseDto
    {
        public string Ssn { get; set; }      
        public string PRMedicalLicense { get; set; }
        public string LastName { get; set; }

        public string SurName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int DelegateId { get; set;}

    }
}
